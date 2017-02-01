#include "ComHelper.h"
#include <objbase.h>


ComHelper::ComHelper()
{
	CoInitialize(NULL);
}


ComHelper::~ComHelper()
{
	CoUninitialize();
}
