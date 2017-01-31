// FileWatch.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <Windows.h>
#include <iostream>
#include <PathCch.h>
#include <thread>

#include "FileWatcher.h"

EXTERN_C IMAGE_DOS_HEADER __ImageBase;

//HANDLE g_directoryHandle = nullptr;
//BYTE g_buffer[16384];
//DWORD g_bytesUsedInBuffer;
//OVERLAPPED g_overlapped;

//void CALLBACK FileChanged(DWORD errorCode, DWORD numberOfBytesTransfered, LPOVERLAPPED overlapped) {
//	if (errorCode == ERROR_OPERATION_ABORTED)
//	{
//		return;
//	}
//
//}
//
//void WatchFile(const std::wstring& directory, const std::wstring& file) {
//	if (g_directoryHandle == nullptr)
//	{
//		g_directoryHandle = ::CreateFileW(
//			directory.c_str(),					// pointer to the file name
//			FILE_LIST_DIRECTORY,                // access (read/write) mode
//			FILE_SHARE_READ						// share mode
//			| FILE_SHARE_WRITE
//			| FILE_SHARE_DELETE,
//			NULL,                               // security descriptor
//			OPEN_EXISTING,                      // how to create
//			FILE_FLAG_BACKUP_SEMANTICS			// file attributes
//			| FILE_FLAG_OVERLAPPED,
//			NULL);                              // file with attributes to copy
//
//		if (g_directoryHandle == INVALID_HANDLE_VALUE)
//		{
//			throw std::runtime_error("Failed to get directory handle.");
//		}
//	}
//
//	ReadDirectoryChangesW(g_directoryHandle, &g_buffer, sizeof(g_buffer), FALSE, FILE_NOTIFY_CHANGE_LAST_WRITE, &g_bytesUsedInBuffer, &g_overlapped, &FileChanged);
//}

bool g_keepWorkerAlive = true;

int main()
{
	/*std::thread worker{ []() {
		FileWatcherImpl fw{ L"j:\\temp", L"foo.txt", []() {std::cout << "File Changed." << std::endl;} };
		while (g_keepWorkerAlive) {
			fw.WatchFile();
			SleepEx(INFINITE, true);
		}
	}
	};*/



	/*WCHAR   DllPath[MAX_PATH] = { 0 };
	GetModuleFileNameW((HINSTANCE)&__ImageBase, DllPath, _countof(DllPath));
	PathCchRemoveFileSpec(DllPath, MAX_PATH);
	HANDLE waitHandle = FindFirstChangeNotification(DllPath, false, FILE_NOTIFY_CHANGE_LAST_WRITE);
	if (waitHandle != INVALID_HANDLE_VALUE) {
		while (true) {
			WaitForSingleObject(waitHandle, INFINITE);
			std::cout << "File changed." << std::endl;
		}
	}*/
	//	std::wcout << DllPath << std::endl;
	/*char a;
	std::cin >> a;*/
}
//
//int __stdcall DllMain(void* hModule, unsigned long nReason, void* hReserved);
//
//DWORD WINAPI Worker(LPVOID) {
//	HMODULE thisModule;
//	GetModuleHandleEx(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS, (LPCTSTR)DllMain, &thisModule);
//	FileWatcherImpl fw{ L"j:\\temp", L"foo.txt", []() {std::cout << "File Changed." << std::endl;} };
//	while (g_keepWorkerAlive) {
//		fw.WatchFile();
//		SleepEx(INFINITE, true);
//	}
//	FreeLibraryAndExitThread(thisModule, 0);
//}
//
//int __stdcall DllMain(void* hModule, unsigned long nReason, void* hReserved)
//{
//	switch (nReason)
//	{
//	case DLL_PROCESS_ATTACH:
//	{
//		CreateThread(nullptr, 0, Worker, nullptr, 0, nullptr);
//	}
//	break;
//	case DLL_PROCESS_DETACH:
//		//g_keepWorkerAlive = false;
//		break;
//	}
//
//	return 1;
//}