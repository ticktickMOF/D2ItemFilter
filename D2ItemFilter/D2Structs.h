#ifndef D2STRUCTS_H__
#define D2STRUCTS_H__

/********************************************
*	D2Structs.h								*
*											*
*	This file is meant to declare structs	*
*	of the game, all in one file. Keep it	*
*	organized to simplify your life. Keep	*
*	the structs inalphabetical order and	*
*	give self descriving names to the		*
*	structs and their memebers				*
*											*
*********************************************/

#include "D2DataTables.h"
#include "D2PacketDef.h"

#pragma pack(1)

///////////////////
// DECLARATIONS ///
///////////////////

struct D2ItemDataStrc;
struct D2UnitStrc;

//////////////////
// DEFINITIONS ///
//////////////////

struct D2ItemDataStrc
{
	DWORD dwQuality;
};

struct D2UnitStrc
{
	DWORD dwUnitType;
	DWORD dwClass;
	void* pMemoryPool;
	DWORD dwGUID;
	DWORD dwMode;
	D2ItemDataStrc* pItemData;
};

#pragma pack()
#endif