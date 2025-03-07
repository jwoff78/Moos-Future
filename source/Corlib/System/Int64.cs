﻿namespace System
{
	public unsafe struct Int64
	{
        private long _value;

        public const long MaxValue = 9223372036854775807L;

        public const long MinValue = -9223372036854775808L;

        public override string ToString()
		{
			long val = this;
			bool isNeg = BitHelpers.IsBitSet(val, 63);
			char* x = stackalloc char[22];
			int i = 20;

			x[21] = '\0';

			if (isNeg)
			{
				ulong _val = (ulong)val;
				_val = 0xFFFFFFFFFFFFFFFF - _val;
				_val += 1;
				val = (long)_val;
			}

			do
			{
				long d = val % 10;
				val /= 10;

				d += 0x30;
				x[i--] = (char)d;
			} while (val > 0);

			if (isNeg)
			{
				x[i] = '-';
			} else
			{
				i++;
			}

			return new string(x + i, 0, 21 - i);
		}
	}
}