
namespace System
{
    public struct UInt32
    {
        private uint _value;

        public const uint MaxValue = 4294967295u;

        public const uint MinValue = 0u;

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
