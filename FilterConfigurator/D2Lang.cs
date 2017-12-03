using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter
{
    static class D2Lang
    {
        [DllImport("d2lang.dll", EntryPoint = "#10003", CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Unicode)]
        [return:MarshalAs(UnmanagedType.LPTStr)]
        public static extern string GetNameString(int nameId);
    }
}
