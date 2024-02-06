using Internal.Runtime.CompilerServices;

namespace Internal.Runtime
{
    internal readonly struct IatAwarePointer<T> where T : unmanaged
    {
        private unsafe readonly T* _value;

        public unsafe T* Value
        {
            get
            {
                if (((int)_value & 1) == 0)
                {
                    return _value;
                }
                return *(T**)((byte*)_value - 1);
            }
        }
    }

    internal readonly struct IatAwareRelativePointer<T> where T : unmanaged
    {
        private readonly int _value;

        public unsafe T* Value
        {
            get
            {
                if ((_value & 1) == 0)
                {
                    return (T*)((byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in _value)) + _value);
                }
                return *(T**)((byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in _value)) + (_value & -2));
            }
        }
    }
}
