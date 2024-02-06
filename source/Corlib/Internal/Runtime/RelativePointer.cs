﻿using Internal.Runtime.CompilerServices;
using System;

namespace Internal.Runtime
{
    internal readonly struct RelativePointer
    {
        private readonly int _value;

        public unsafe IntPtr Value => (IntPtr)((byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in _value)) + _value);
    }

    internal readonly struct RelativePointer<T> where T : unmanaged
    {
        private readonly int _value;

        public unsafe T* Value => (T*)((byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in _value)) + _value);
    }

}
