using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter
{
    class NativeMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool DropFilterCallback(ref D2UnitAnyStrc arg);

        [DllImport("DropFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RegisterDropFilterCallback([MarshalAs(UnmanagedType.FunctionPtr)] DropFilterCallback fn);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        
    }

}
