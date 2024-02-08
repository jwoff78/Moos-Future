
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
    public static class Debug
    {
        //temp
        [DllImport("*")]
        private static extern void Panic(string message);

        public static void WriteLine(string s) 
        {
            for(int i = 0; i < s.Length; i++) 
            {
                DebugWrite(s[i]);
            }
            DebugWriteLine();
            s.Dispose();
        }

        public static void WriteLine()
        {
            DebugWriteLine();
        }

        public static void Write(char c)
        {
            DebugWrite(c);
        }

        public static void Write(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                DebugWrite(s[i]);
            }
            s.Dispose();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [Conditional("DEBUG")]
        internal static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                //RhFailFastReason.InternalError = 1
                Panic("InternalError");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [Conditional("DEBUG")]
        internal static void Assert(bool condition)
        {
            if (!condition)
            {
                Panic("InternalError");
            }
        }

        [DllImport("*")]
        static extern void DebugWrite(char c);

        [DllImport("*")]
        static extern void DebugWriteLine();
    }
}
