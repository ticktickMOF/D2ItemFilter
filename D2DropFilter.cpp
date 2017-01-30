#include "DLLmain.h"

BOOL __fastcall DROPFILTER_Main(D2UnitStrc* pItem)
{
	if (pItem->pItemData == NULL)
		return FALSE;

	if (GetAsyncKeyState(VK_CONTROL) & 0x8000)
		return TRUE;

	//UNIQUE & SET ITEMS
	if (pItem->pItemData->dwQuality == 0x07 || pItem->pItemData->dwQuality == 0x05)
		return TRUE;

	D2ItemsTXT* pItemTxt = D2COMMON_GetItemTxtRecord(pItem->dwClass);
	if (pItemTxt == NULL){ return FALSE; }

	DWORD dwCode = pItemTxt->dwCode;

	//WIRTS LEG & CUBE
	if (dwCode == CODE32('leg ') || dwCode == CODE32('box '))
	{
		return TRUE;
	}

	//GREAT RUNES
	if (dwCode == CODE32('r51 ') || dwCode == CODE32('r52 ') || dwCode == CODE32('r53 ') ||
	dwCode == CODE32('r54 ') || dwCode == CODE32('r55 ') || dwCode == CODE32('r56 ') ||
	dwCode == CODE32('r57 ') || dwCode == CODE32('r58 ') || dwCode == CODE32('r59 ') ||
	dwCode == CODE32('r60 ') || dwCode == CODE32('r61 ') || dwCode == CODE32('r62 '))
	{
		return TRUE;
	}


	//SIGNETS
	if (dwCode == CODE32('zkb ') || dwCode == CODE32('zkf ') || dwCode == CODE32('zku ') || dwCode == CODE32('zkr ') || 
	dwCode == CODE32('!@B ') || dwCode == CODE32('zkw ') || dwCode == CODE32('zkq ') || dwCode == CODE32('zkx ') || 
	dwCode == CODE32('zky ') || dwCode == CODE32('zka ') || dwCode == CODE32('zkc ') || dwCode == CODE32('zkd ') || 
	dwCode == CODE32('zke ') || dwCode == CODE32('zk# '))
	{
		return TRUE;
	}

	//TROPHIES
	if (dwCode == 0x203130B5 || dwCode == 0x203230B5 || dwCode == 0x203330B5 ||
	dwCode == 0x203430B5 || dwCode == 0x203530B5 || dwCode == 0x203630B5 || 
	dwCode == 0x203730B5 || dwCode == 0x203830B5 || dwCode == 0x203930B5 ||
	dwCode == 0x203031B5 || dwCode == 0x203131B5 || dwCode == 0x203231B5 ||
	dwCode == 0x203331B5 || dwCode == 0x203431B5 || dwCode == 0x203531B5 ||
	dwCode == CODE32('bxt ') || dwCode == CODE32('##/ '))
	{
		return TRUE;
	}


	//UNIQUE MYSTIC ORBS
	if (dwCode == CODE32('&66 ') || dwCode == CODE32('&67 ') || dwCode == CODE32('&68 ') ||
	dwCode == CODE32('&69 ') || dwCode == CODE32('&70 ') || dwCode == CODE32('&71 ') ||
	dwCode == CODE32('&72 ') || dwCode == CODE32('&73 ') || dwCode == CODE32('&74 ') ||
	dwCode == CODE32('&75 ') || dwCode == CODE32('&76 ') || dwCode == CODE32('&77 ') ||
	dwCode == CODE32('&78 ') || dwCode == CODE32('&79 ') || dwCode == CODE32('&80 ') ||
	dwCode == CODE32('&81 ') || dwCode == CODE32('&82 ') || dwCode == CODE32('&83 ') ||
	dwCode == CODE32('&84 ') || dwCode == CODE32('&85 ') || dwCode == CODE32('&86 ') ||
	dwCode == CODE32('&87 ') || dwCode == CODE32('&88 ') || dwCode == CODE32('&89 ') ||
	dwCode == CODE32('&90 ') || dwCode == CODE32('&91 ') || dwCode == CODE32('&92 ') ||
	dwCode == CODE32('&93 ') || dwCode == CODE32('&94 ') || dwCode == CODE32('&95 '))
	{
		return TRUE;
	}

	//CYCLES
	if (dwCode == CODE32('cm1 ') || dwCode == CODE32('cm2 ') || dwCode == CODE32('cm3 ') || dwCode == CODE32('\\6 '))
	{
		return TRUE;
	}

	//SEWER ITEMS
	if (dwCode == CODE32('r99 ') || dwCode == CODE32('Kx1 ') || dwCode == CODE32('Kx2 ') ||
	dwCode == CODE32('Kx3 ') || dwCode == CODE32('Kx4 ') || dwCode == CODE32('Kx5 ') ||
	dwCode == CODE32('Kx6 ') || dwCode == CODE32('r98 '))
	{	
		return TRUE;
	}

	//KABRAXIS GEMS
	if (dwCode == CODE32('\\1 ') || dwCode == CODE32('\\2 ') || dwCode == CODE32('\\3 ') ||
	dwCode == CODE32('\\4 ') || dwCode == CODE32('\\5 '))
	{	
		return TRUE;
	}

	//RARE CUBE REAGANTS
	if (dwCode == CODE32('||* ') || dwCode == CODE32('||# ') || dwCode == CODE32('||l ') ||
	dwCode == CODE32('||` ') || dwCode == CODE32('||. ') || dwCode == CODE32('||_ ') ||
	dwCode == CODE32('`^# ') || dwCode == CODE32('r97 ') || dwCode == CODE32('#2^ ') ||
	dwCode == CODE32('#X9 ') || dwCode == CODE32('#X1 ') || dwCode == CODE32('#X2 ') ||
	dwCode == CODE32('#X3 ') || dwCode == CODE32('#X4 ') || dwCode == CODE32('#X5 ') ||
	dwCode == CODE32('#X6 ') || dwCode == CODE32('@## ') || dwCode == CODE32('#X91') || 
	dwCode == CODE32('c@c ') || dwCode == CODE32('b@b ') || dwCode == CODE32('l@l ') || 
	dwCode == CODE32('m@m ') || dwCode == CODE32('o@o ') || dwCode == CODE32('p@p ') || 
	dwCode == CODE32('q@q ') || dwCode == CODE32('j34 ') || dwCode == CODE32('g34 ') || 
	dwCode == CODE32('bbb ') || dwCode == CODE32('ass ') || dwCode == CODE32('tr2 ') || 
	dwCode == CODE32('ice '))
	{	
		return TRUE;
	}

	//BRAINS & TOKENS
	if (dwCode == CODE32('2x5 ') || dwCode == CODE32('2x6 ') || dwCode == CODE32('2x7 ') ||
	dwCode == CODE32('2x8 ') || dwCode == CODE32('2x9 ') || dwCode == CODE32('!@1 ') || dwCode == CODE32('!@2 ') || 
	dwCode == CODE32('!@3 ') || dwCode == CODE32('!@4 ') || dwCode == CODE32('!@5 ') || dwCode == CODE32('!@6 ') ||
	dwCode == CODE32('!@7 ') || dwCode == CODE32('!@8 ') || dwCode == CODE32('!@9 ') || dwCode == CODE32('!@A '))
	{	
		return TRUE;
	}

	//VOID ITEMS
	if (dwCode == CODE32('voi1') || dwCode == CODE32('voi2') || dwCode == CODE32('voi3') ||
	dwCode == CODE32('voi4') || dwCode == CODE32('voi5') || dwCode == CODE32('voi6') || dwCode == CODE32('vor1') || 
	dwCode == CODE32('vor2') || dwCode == CODE32('voDX'))
	{	
		return TRUE;
	}

	return FALSE;
}