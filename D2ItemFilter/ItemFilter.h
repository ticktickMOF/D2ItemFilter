#pragma once

#include <set>
#include <intsafe.h>

struct ItemCriteria {
	DWORD quality;
	DWORD code;
};

class ItemFilter
{
public:
	ItemFilter();
	~ItemFilter();

	void AddQuality(DWORD quality);
	void AddCode(DWORD code);
	bool IsIncluded(DWORD quality, DWORD code);
private:
	std::set<int> _itemQuality{};
	std::set<int> _itemCodes{};
};

