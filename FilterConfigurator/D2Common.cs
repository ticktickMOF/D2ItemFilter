using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter
{
    static class D2Common
    {
        private static NonExportedLayer.Module @base = new NonExportedLayer.Module(NativeMethods.GetModuleHandle("D2Common.dll"));

        private delegate IntPtr GetItemTextRecordDelegate(Int32 itemClass);
        private static GetItemTextRecordDelegate _GetItemTextRecord = @base.GetDelegateFromOffset<GetItemTextRecordDelegate>(0x719A0);
        public static D2ItemsTXT GetItemTextRecord(Int32 itemClass) { return Marshal.PtrToStructure<D2ItemsTXT>(_GetItemTextRecord(itemClass)); }

        //public static D2ItemsTXT[] GetAllItemTextRecords()
        //{
        //    @base.
        //}
    }
}
