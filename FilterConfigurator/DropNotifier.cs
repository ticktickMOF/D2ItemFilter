using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter
{
    class DropNotifier
    {
        static private int earOffset = typeof(D2ItemDataStrc).GetField("bEar").GetCustomAttributes(typeof(System.Runtime.InteropServices.FieldOffsetAttribute), true).Cast<System.Runtime.InteropServices.FieldOffsetAttribute>().First().Value;

        static public void DoScan()
        {
            var filter = FilterReloader.Instance.NotificationFilter;
            if (filter == null)
            {
                return;
            }
            var player = D2Client.Instance.PlayerUnit;
            
            foreach (var room in player?.Path?.Room1?.RoomsNear ?? Enumerable.Empty<D2Room1Strc>())
            {
                var unitPtr = room.pUnitFirst;
                while (unitPtr != IntPtr.Zero)
                {
                    var unit = System.Runtime.InteropServices.Marshal.PtrToStructure<D2UnitAnyStrc>(unitPtr);
                    if (unit.dwUnitType == 4)
                    {
                        //ear is our "i've seen it" marker
                        if (unit.ItemData.bEar == 0)
                        {
                            var itemTxt = D2Common.GetItemTextRecord(unit.dwClass);
                            if (filter[itemTxt.szCode, unit.ItemData.dwQuality])
                            {
                                StringBuilder namestr = new StringBuilder(30);
                                //D2Client.Instance.GetItemName(unitPtr, namestr, namestr.Capacity);
                                var name = D2Lang.GetNameString(itemTxt.nNameStrId);
                                name = string.Join("    ", name.Split('\n').Reverse());
                                //D2Client.Instance.GamePrint(QualityToColorCode(unit.ItemData.dwQuality), namestr.ToString());
                                D2Client.Instance.GamePrint(QualityToColorCode(unit.ItemData.dwQuality), name.Replace("\n", "    "));
                                System.Runtime.InteropServices.Marshal.WriteByte(unit.pItemData + earOffset, 1);
                            }
                        }

                    }
                    unitPtr = unit.pListNext;
                }
            }

        }

        static public int QualityToColorCode(ItemQuality quality)
        {
            switch (quality)
            {
                case ItemQuality.Low:
                case ItemQuality.Normal:
                case ItemQuality.Superior:
                    return 0;
                case ItemQuality.Magic:
                    return 3;
                case ItemQuality.Rare:
                    return 9;
                case ItemQuality.Set:
                    return 2;
                case ItemQuality.Unique:
                    return 4;
                case ItemQuality.Crafted:
                    return 8;
                case ItemQuality.Honorific:
                    return 12;
                default:
                    return 0;
            }
        }
    }
}
