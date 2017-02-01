#pragma once

#include <string>
#include <Windows.h>
#include <functional>

class FileWatcher
{
public:
	FileWatcher(const std::wstring& directory, const std::wstring& filename, std::function<void(void)> callback);
	~FileWatcher();
	void WatchFile();
private:
	std::wstring _directory;
	std::wstring _filename;
	HANDLE _directoryHandle = nullptr;
	BYTE _buffer[10000];
	OVERLAPPED _overlapped{};
	DWORD _bytesReturned = 0;
	bool _shouldPushUpdate = false;
	std::function<void(void)> _callback{};

	static void CALLBACK FileChanged(DWORD errorCode, DWORD numberOfBytesTransfered, LPOVERLAPPED overlapped);
};

