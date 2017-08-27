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
	enum itemQualities : DWORD {
		QUALITY_LOW = 1,
		QUALITY_NORMAL = 2,
		QUALITY_SUPERIOR = 3,
		QUALITY_MAGIC = 4,
		QUALITY_SET = 5,
		QUALITY_RARE = 6,
		QUALITY_UNIQUE = 7,
		QUALITY_CRAFTED = 8,
		QUALITY_TAMPERED = 9
	} dwQuality;
	struct D2ItemSeed {
		DWORD lowSeed;
		DWORD highSeed;
	} itemSeed;
	DWORD ownerGUID;
	DWORD fingerprint;
	DWORD commandFlags;
	enum itemFlags : DWORD {
		FLAGS_SOCKETED = 0x00000800,
		FLAGS_ETHERAL = 0x00400000,
		FLAGS_RUNEWORD = 0x04000000
	} itemFlags;
	// more omitted
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