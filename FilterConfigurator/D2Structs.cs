using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter
{
    public enum ItemQuality : Int32
    {
        Low = 1,
        Normal = 2,
        Superior = 3,
        Magic = 4,
        Set = 5,
        Rare = 6,
        Unique = 7,
        Crafted = 8,
        Honorific = 9
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    struct D2ItemDataStrc
    {
        [FieldOffset(0x0)]
        public ItemQuality dwQuality;
        [FieldOffset(0x48)]
        public byte bEar;
    }

    [StructLayout(LayoutKind.Explicit)]
    struct D2UnitAnyStrc
    {
        [FieldOffset(0x0)]
        public Int32 dwUnitType;
        [FieldOffset(0x4)]
        public Int32 dwClass;
        [FieldOffset(0x0C)]
        public Int32 dwGUID;
        [FieldOffset(0x14)]
        public IntPtr pItemData;

        public D2ItemDataStrc ItemData { get { return Marshal.PtrToStructure<D2ItemDataStrc>(pItemData); } }

        [FieldOffset(0x2c)]
        private IntPtr pPath;

        public D2PathStrc? Path { get { return pPath == IntPtr.Zero ? (D2PathStrc?)null : Marshal.PtrToStructure<D2PathStrc>(pPath); } }

        [FieldOffset(0xE8)]
        public IntPtr pListNext;

        //public D2UnitAnyStrc ListNext { get { return Marshal.PtrToStructure<D2UnitAnyStrc>(pListNext); } }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct D2PathStrc
    {
        [FieldOffset(0x1C)]
        private IntPtr pRoom1;

        public D2Room1Strc? Room1 { get { return pRoom1 == IntPtr.Zero ? (D2Room1Strc?)null : Marshal.PtrToStructure<D2Room1Strc>(pRoom1); } }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct D2Room1Strc //: IEnumerable<D2UnitAnyStrc>
    {
        [FieldOffset(0x0)]
        public IntPtr pRoomsNear;

        public D2Room1Strc[] RoomsNear
        {
            get
            {
                D2Room1Strc[] ret = new D2Room1Strc[RoomsNearCount];
                for (int i = 0; i < RoomsNearCount; i++)
                {
                    var ptr = Marshal.ReadIntPtr(pRoomsNear + 4 * i);
                    ret[i] = Marshal.PtrToStructure<D2Room1Strc>(ptr);
                }
                return ret;
            }
        }

        [FieldOffset(0x24)]
        public int RoomsNearCount;

        [FieldOffset(0x74)]
        public IntPtr pUnitFirst;

        //public IEnumerator<D2UnitAnyStrc> GetEnumerator()
        //{
        //    IntPtr unitPtr = pUnitFirst;
        //    while(unitPtr != IntPtr.Zero)
        //    {
        //        var entry = Marshal.PtrToStructure<D2UnitAnyStrc>(unitPtr);
        //        yield return entry;
        //        unitPtr = entry.pListNext;
        //    }
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return ((IEnumerable<D2UnitAnyStrc>)this).GetEnumerator();
        //}
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Pack = 1)]
    struct D2ItemsTXT
    {
        [FieldOffset(0x80)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        private char[] code;

        public string szCode { get { return new string(code); } }


        //public int dwCode
        //{
        //    get
        //    {
        //        int ret = 0;
        //        for (int i = 0; i < szCode.Length; i++)
        //        {
        //            byte b = (byte)szCode[i];
        //            ret += ((int)b) << (i * 8);
        //        }
        //        return ret;
        //    }
        //}

        [FieldOffset(0x0F4)]
        public int nNameStrId;

        //... more omitted
    }
}
