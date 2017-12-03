/********************************************
*	D2Patch.h								*
*											*
*	All patches are defined in this file.	*
*	As always try to keep it organized.		*
*	Keep patches that are done in the same	*
*	DLL grouped together, etc...			*
*											*
*********************************************/

static const CodePatch gCodePatches[] =
{
	{D2DLL_D2CLIENT,	DLLBASE_D2CLIENT+0x5907E,	(DWORD)0x000000E9,			0},
	{D2DLL_D2CLIENT,	DLLBASE_D2CLIENT+0x5907F,	(DWORD)PATCH_DropFilter,	1},

	{D2DLL_INVALID},	//Must be the last entry in the array!
};

static const SmallCodePatch gSmallCodePatches[] =
{
    //{ D2DLL_MXL,	DLLBASE_MXL + 0x14df,	(BYTE)0xEB,	0 },//disable crash when debugger is attached
    //{ D2DLL_MXL, DLLBASE_MXL + 0xab50 , (BYTE) 0, 0},//disable debugger message spam

    { D2DLL_INVALID },	//Must be the last entry in the array!
};