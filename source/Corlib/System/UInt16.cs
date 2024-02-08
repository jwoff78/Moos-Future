
namespace System
{
    public struct UInt16 
    {
        private ushort _value;

        public const ushort MaxValue = 65535;

        public const ushort MinValue = 0;

        public unsafe override string ToString()
        {
            return ((ulong)this).ToString();
        }

        public string ToString(string format)
        {
            return ((ulong)this).ToString(format);
        }
    }
}
