using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNumeric(this Type type)
        {
            if (type == null)
                return false;

            // Check if the type is any of the numeric types
            return type == typeof(byte) ||
                   type == typeof(sbyte) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type == typeof(int) ||
                   type == typeof(uint) ||
                   type == typeof(long) ||
                   type == typeof(ulong) ||
                   type == typeof(float) ||
                   type == typeof(double) ||
                   type == typeof(decimal);
        }

        public static object CastNumber(this Type type, decimal value)
        {
            if (type == typeof(byte)) return (byte)value;
            if (type == typeof(sbyte)) return (sbyte)value;
            if (type == typeof(short)) return (short)value;
            if (type == typeof(ushort)) return (ushort)value;
            if (type == typeof(int)) return (int)value;
            if (type == typeof(uint)) return (uint)value;
            if (type == typeof(long)) return (long)value;
            if (type == typeof(ulong)) return (ulong)value;
            if (type == typeof(float)) return (float)value;
            if (type == typeof(double)) return (double)value;
            return value;
        }

        public static bool IsDate(this Type type)
        {
            if (type == null)
                return false;
            return type == typeof(DateTime);
        }
    }
}
