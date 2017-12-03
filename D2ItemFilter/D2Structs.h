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
//
//struct UnitAny;
//
//struct Room1 {
//    Room1** pRoomsNear;    //0x00
//    DWORD _1[3];         //0x04
//    void* pRoom2;         //0x10
//    DWORD _2[3];         //0x14
//    void* Coll;         //0x20
//    DWORD dwRoomsNear;      //0x24
//    DWORD _3[9];         //0x28
//    DWORD dwPosX;         //0x4C
//    DWORD dwPosY;         //0x50
//    DWORD dwSizeX;         //0x54
//    DWORD dwSizeY;         //0x58
//    DWORD _4[6];         //0x5C
//    UnitAny* pUnitFirst;   //0x74
//    DWORD _5;            //0x78
//    Room1* pRoomNext;      //0x7C
//};
//
//struct Path {
//    WORD xOffset;               //0x00
//    WORD xPos;                  //0x02
//    WORD yOffset;               //0x04
//    WORD yPos;                  //0x06
//    DWORD _1[2];               //0x08
//    WORD xTarget;               //0x10
//    WORD yTarget;               //0x12
//    DWORD _2[2];               //0x14
//    Room1 *pRoom1;               //0x1C
//    Room1 *pRoomUnk;            //0x20
//    DWORD _3[3];               //0x24
//    UnitAny *pUnit;               //0x30
//    DWORD dwFlags;               //0x34
//    DWORD _4;                  //0x38
//    DWORD dwPathType;            //0x3C
//    DWORD dwPrevPathType;         //0x40
//    DWORD dwUnitSize;            //0x44
//    DWORD _5[4];               //0x48
//    UnitAny *pTargetUnit;         //0x58
//    DWORD dwTargetType;            //0x5C
//    DWORD dwTargetId;            //0x60
//    BYTE bDirection;            //0x64
//};
//
//struct UnitAny {
//    DWORD dwType;               //0x00
//    DWORD dwTxtFileNo;            //0x04
//    DWORD _1;                  //0x08
//    DWORD dwUnitId;               //0x0C
//    DWORD dwMode;               //0x10
//    union
//    {
//        PlayerData *pPlayerData;
//        ItemData *pItemData;
//        MonsterData *pMonsterData;
//        ObjectData *pObjectData;
//        //TileData *pTileData doesn't appear to exist anymore
//    };                        //0x14
//    DWORD dwAct;               //0x18
//    void *pAct;                  //0x1C
//    DWORD dwSeed[2];            //0x20
//    DWORD _2;                  //0x28
//    Path *pPath;                //0x2C
//    DWORD _3[5];               //0x30
//    DWORD dwGfxFrame;            //0x44
//    DWORD dwFrameRemain;         //0x48
//    WORD wFrameRate;            //0x4C
//    WORD _4;                  //0x4E
//    BYTE *pGfxUnk;               //0x50
//    DWORD *pGfxInfo;            //0x54
//    DWORD _5;                  //0x58
//    StatList *pStats;            //0x5C
//    Inventory *pInventory;         //0x60
//    Light *ptLight;               //0x64
//    DWORD _6[9];               //0x68
//    WORD wX;                  //0x8C
//    WORD wY;                  //0x8E
//    DWORD _7;                  //0x90
//    DWORD dwOwnerType;            //0x94
//    DWORD dwOwnerId;            //0x98
//    DWORD _8[2];               //0x9C
//    OverheadMsg* pOMsg;            //0xA4
//    Info *pInfo;               //0xA8
//    DWORD _9[6];               //0xAC
//    DWORD dwFlags;               //0xC4
//    DWORD dwFlags2;               //0xC8
//    DWORD _10[5];               //0xCC
//    UnitAny *pChangedNext;         //0xE0
//    UnitAny *pRoomNext;            //0xE4
//    UnitAny *pListNext;            //0xE8 -> 0xD8
//    char szNameCopy[0x10]; //+0x66
//};


#pragma pack()
#endif