using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter
{
    class NonExportedLayer
    {
        public class Structure
        {
            public Structure(IntPtr baseAddress)
            {
                BaseAddress = baseAddress;
            }

            public Structure()
                : this(IntPtr.Zero)
            {

            }

            public IntPtr BaseAddress { get; private set; }

            protected IntPtr GetOffset(UInt32 offset)
            {
                return (IntPtr)(BaseAddress.ToInt32() + offset);
            }

            protected Byte[] ReadBytes(UInt32 offset, Int32 count)
            {
                Byte[] buffer = new Byte[count];
                Marshal.Copy(GetOffset(offset), buffer, 0, count);
                return buffer;
            }

            protected void WriteBytes(UInt32 offset, Byte[] value)
            {
                Marshal.Copy(value, 0, GetOffset(offset), value.Length);
            }

            protected Byte ReadByte(UInt32 offset)
            {
                return Marshal.ReadByte(GetOffset(offset));
            }

            protected void WriteByte(UInt32 offset, Byte value)
            {
                Marshal.WriteByte(GetOffset(offset), value);
            }

            protected Int16 ReadInt16(UInt32 offset)
            {
                return Marshal.ReadInt16(GetOffset(offset));
            }

            protected void WriteInt16(UInt32 offset, Int16 value)
            {
                Marshal.WriteInt16(GetOffset(offset), value);
            }

            protected UInt16 ReadUInt16(UInt32 offset)
            {
                return BitConverter.ToUInt16(ReadBytes(offset, sizeof(UInt16)), 0);
            }

            protected void WriteUInt16(UInt32 offset, UInt16 value)
            {
                WriteBytes(offset, BitConverter.GetBytes(value));
            }

            protected Int32 ReadInt32(UInt32 offset)
            {
                return Marshal.ReadInt32(GetOffset(offset));
            }

            protected void WriteInt32(UInt32 offset, Int32 value)
            {
                Marshal.WriteInt32(GetOffset(offset), value);
            }

            protected UInt32 ReadUInt32(UInt32 offset)
            {
                return BitConverter.ToUInt32(ReadBytes(offset, sizeof(UInt32)), 0);
            }

            protected void WriteUInt32(UInt32 offset, UInt32 value)
            {
                WriteBytes(offset, BitConverter.GetBytes(value));
            }

            protected Int64 ReadInt64(UInt32 offset)
            {
                return Marshal.ReadInt32(GetOffset(offset));
            }

            protected void WriteInt64(UInt32 offset, Int64 value)
            {
                Marshal.WriteInt64(GetOffset(offset), value);
            }

            protected UInt64 ReadUInt64(UInt32 offset)
            {
                return BitConverter.ToUInt64(ReadBytes(offset, sizeof(UInt64)), 0);
            }

            protected void WriteUInt64(UInt32 offset, UInt64 value)
            {
                WriteBytes(offset, BitConverter.GetBytes(value));
            }

            protected IntPtr ReadIntPtr(UInt32 offset)
            {
                return Marshal.ReadIntPtr(GetOffset(offset));
            }

            protected void WriteIntPtr(UInt32 offset, IntPtr value)
            {
                Marshal.WriteIntPtr(GetOffset(offset), value);
            }

            protected T ReadPointer<T>(UInt32 offset) where T : Structure, new()
            {
                IntPtr pointer = ReadIntPtr(offset);

                if (pointer == IntPtr.Zero)
                {
                    return null;
                }

                T instance = new T();
                instance.BaseAddress = pointer;
                return instance;
            }

            protected void WritePointer(UInt32 offset, Structure value)
            {
                WriteIntPtr(offset, value.BaseAddress);
            }

            protected String ReadAnsiString(UInt32 offset)
            {
                return Marshal.PtrToStringAnsi(GetOffset(offset));
            }

            protected void WriteAnsiString(UInt32 offset, String value)
            {
                WriteBytes(offset, Encoding.ASCII.GetBytes(value));
            }

            protected String ReadUnicodeString(UInt32 offset)
            {
                return Marshal.PtrToStringUni(GetOffset(offset));
            }

            protected void WriteUnicodeString(UInt32 offset, String value)
            {
                WriteBytes(offset, Encoding.Unicode.GetBytes(value));
            }
        }


        public class Module : Structure
        {
            public Module(IntPtr baseAddress)
                : base(baseAddress)
            {

            }

            public T GetDelegateFromOffset<T>(UInt32 offset) where T : class
            {
                if(BaseAddress == IntPtr.Zero)
                {
                    return default(T);
                }
                return Marshal.GetDelegateForFunctionPointer(
                   GetOffset(offset),
                   typeof(T)) as T;
            }

            
        }
    }
}
