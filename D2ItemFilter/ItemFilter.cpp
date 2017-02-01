#include "ItemFilter.h"



ItemFilter::ItemFilter()
{
}


ItemFilter::~ItemFilter()
{
}

void ItemFilter::AddQuality(DWORD quality)
{
	_itemQuality.insert(quality);
}

void ItemFilter::AddCode(DWORD code)
{
	_itemCodes.insert(code);
}

bool ItemFilter::IsIncluded(DWORD quality, DWORD code)
{
	return (_itemCodes.find(code) != _itemCodes.end()) || (_itemQuality.find(quality) != _itemQuality.end());
}
