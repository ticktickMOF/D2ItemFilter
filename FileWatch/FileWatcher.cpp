#include "stdafx.h"
#include "FileWatcher.h"
#include <Shlwapi.h>




FileWatcher::FileWatcher(const std::wstring& directory, const std::wstring& filename, std::function<void(void)> callback) : _directory(directory), _filename(filename), _callback(callback)
{
	if (_directoryHandle == nullptr)
	{
		_directoryHandle = ::CreateFileW(
			_directory.c_str(),					// pointer to the file name
			FILE_LIST_DIRECTORY,                // access (read/write) mode
			FILE_SHARE_READ						// share mode
			| FILE_SHARE_WRITE
			| FILE_SHARE_DELETE,
			NULL,                               // security descriptor
			OPEN_EXISTING,                      // how to create
			FILE_FLAG_BACKUP_SEMANTICS			// file attributes
			| FILE_FLAG_OVERLAPPED,
			NULL);                              // file with attributes to copy

		if (_directoryHandle == INVALID_HANDLE_VALUE)
		{
			throw std::runtime_error("Failed to get directory handle.");
		}
	}
	_overlapped.hEvent = this;
}

FileWatcher::~FileWatcher()
{
	CloseHandle(_directoryHandle);
}

void FileWatcher::WatchFile()
{
	ReadDirectoryChangesW(_directoryHandle, &_buffer, sizeof(_buffer), FALSE, FILE_NOTIFY_CHANGE_LAST_WRITE, &_bytesReturned, &_overlapped, &FileChanged);
	//make sure events are finished, events can be noisy depending on the size of the change.
	while (0 != SleepEx(50, true)) {
		ReadDirectoryChangesW(_directoryHandle, &_buffer, sizeof(_buffer), FALSE, FILE_NOTIFY_CHANGE_LAST_WRITE, &_bytesReturned, &_overlapped, &FileChanged);
	}
	if (_shouldPushUpdate) {
		_callback();
		_shouldPushUpdate = false;
	}
}

void CALLBACK FileWatcher::FileChanged(DWORD errorCode, DWORD numberOfBytesTransfered, LPOVERLAPPED overlapped)
{
	FileWatcher* watcher = static_cast<FileWatcher*>(overlapped->hEvent);
	for (FILE_NOTIFY_INFORMATION *info = reinterpret_cast<FILE_NOTIFY_INFORMATION*>(&watcher->_buffer), *previous = 0; info != previous; previous = info, info += info->NextEntryOffset)
	{
		if (info->Action != FILE_ACTION_MODIFIED)
			continue;
		std::wstring filename{ info->FileName, info->FileNameLength / sizeof(wchar_t) };
		if (filename == watcher->_filename) {
			watcher->_shouldPushUpdate = true;
		}
	}

}