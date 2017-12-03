#include "CLRHost.h"

#pragma region Includes and Imports
#include <windows.h>
#include <comdef.h>

#include <metahost.h>
#pragma comment(lib, "mscoree.lib")

// Import mscorlib.tlb (Microsoft Common Language Runtime Class Library).
#import "mscorlib.tlb" raw_interfaces_only				\
    high_property_prefixes("_get","_put","_putref")		\
    rename("ReportEvent", "InteropServices_ReportEvent")
using namespace mscorlib;
#pragma endregion

namespace {
    _COM_SMARTPTR_TYPEDEF(ICLRMetaHost, __uuidof(ICLRMetaHost));
    _COM_SMARTPTR_TYPEDEF(ICLRRuntimeInfo, __uuidof(ICLRRuntimeInfo));
    _COM_SMARTPTR_TYPEDEF(ICLRRuntimeHost, __uuidof(ICLRRuntimeHost));
}

HRESULT CLRHost::CallFunction(const wchar_t* assemblyPath, const wchar_t* className, const wchar_t* functionName)
{
    HRESULT hr;

    ICLRMetaHostPtr pMetaHost;
    hr = CLRCreateInstance(CLSID_CLRMetaHost,
        IID_ICLRMetaHost, (LPVOID*)&pMetaHost);
    if (FAILED(hr)) {
        return hr;
    }

    ICLRRuntimeInfoPtr pRuntimeInfo;
    hr = pMetaHost->GetRuntime(L"v4.0.30319", IID_ICLRRuntimeInfo, (LPVOID*)&pRuntimeInfo);
    if (FAILED(hr)) {
        return hr;
    }

    BOOL loadable;
    hr = pRuntimeInfo->IsLoadable(&loadable);
    if(FAILED(hr)) {
        return hr;
    }

    ICLRRuntimeHostPtr pRuntimeHost;
    hr = pRuntimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (LPVOID*)&pRuntimeHost);
    if (FAILED(hr)) {
        return hr;
    }

    hr = pRuntimeHost->Start();
    if (FAILED(hr)) {
        return hr;
    }

    hr = pRuntimeHost->ExecuteInDefaultAppDomain(assemblyPath, className, functionName, L"", nullptr);
    if (FAILED(hr)) {
        return hr;
    }

    return hr;
}



CLRHost::CLRHost()
{
}


CLRHost::~CLRHost()
{
}
