#include <windows.h>
#include "D2Ptrs.h"
#include "D2Structs.h"
#include <vector>
#include "CLRHost.h"
#include "Shlwapi.h"


EXTERN_C IMAGE_DOS_HEADER __ImageBase;

namespace {



	using DropFilterCallback = bool(D2UnitStrc*);

	std::vector<DropFilterCallback*> g_dropFilterCallbacks;

}



BOOL __fastcall DROPFILTER_Main(D2UnitStrc* pItem)
{
    if (GetAsyncKeyState(VK_CONTROL) & 0x8000)
    	return TRUE;

    for (auto callback : g_dropFilterCallbacks) {
		if (!callback(pItem)) {
			return FALSE;
		}
	}
	return TRUE;

	
}

DWORD WINAPI Worker(LPVOID) {

	wchar_t dllFolder[MAX_PATH];
	GetModuleFileNameW((HINSTANCE)&__ImageBase, dllFolder, _countof(dllFolder));
	PathRemoveFileSpecW(dllFolder);

    
    std::wstring dotNetDll = std::wstring(dllFolder) + L"\\DotNetD2ItemFilter.exe";
    HRESULT hr = CLRHost::CallFunction(dotNetDll.c_str(), L"D2Register", L"RegisterCallbacks");

	return 0;


}

extern "C" __declspec(dllexport) void RegisterDropFilterCallback(DropFilterCallback fn) {
	g_dropFilterCallbacks.push_back(fn);
}