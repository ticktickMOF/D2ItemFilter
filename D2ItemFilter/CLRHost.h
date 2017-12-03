#pragma once

#include <winerror.h>

class CLRHost
{
public:
	CLRHost();
	~CLRHost();

    static HRESULT CallFunction(const wchar_t* assemblyName, const wchar_t* className, const wchar_t* functionName);

};

