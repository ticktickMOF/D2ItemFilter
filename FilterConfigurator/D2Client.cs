using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DotNetD2ItemFilter.NonExportedLayer;

namespace DotNetD2ItemFilter
{
    class D2Client : Module
    {
        public static D2Client Instance = new D2Client();

        private D2Client() : base(NativeMethods.GetModuleHandle("d2client.dll"))
        {
            GamePrintFunction = GetDelegateFromOffset<GamePrintDelegate>(Offset_GamePrint) ?? ((String text, Int32 color) => { });
            GetItemNameFunction = GetDelegateFromOffset<GetItemNameDelegate>(Offset_GetItemName);
        }

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private delegate void GamePrintDelegate(String text, Int32 color);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private delegate void GetItemNameDelegate(IntPtr pUnitAny, StringBuilder buffer, int bufferSize);

        #endregion

        #region Offsets

        private const UInt32 Offset_PlayerUnit = 0x11BBFC;
        private const UInt32 Offset_GamePrint = 0x7D850;
        private const UInt32 Offset_GetItemName = 0x914F0;


        #endregion

        private GamePrintDelegate GamePrintFunction;
        private GetItemNameDelegate GetItemNameFunction;

        public D2UnitAnyStrc? PlayerUnit
        {
            get
            {
                var playerPtr = Marshal.ReadIntPtr(GetOffset(Offset_PlayerUnit));
                if (playerPtr != IntPtr.Zero)
                {
                    return Marshal.PtrToStructure<D2UnitAnyStrc>(playerPtr);
                }
                return null;
            }
        }

        public void GamePrint(Int32 color, String format, params object[] args)
        {
            GamePrint(color, String.Format(format, args));
        }

        public void GamePrint(Int32 color, String message)
        {
            GamePrintFunction(message, color);
        }

        public void GetItemName(IntPtr pUnitAny, StringBuilder buffer, int bufferSize)
        {
            GetItemNameFunction(pUnitAny, buffer, bufferSize);
        }
    }
}
