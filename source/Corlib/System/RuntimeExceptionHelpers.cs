﻿using System.Runtime;

namespace System
{
    internal static class RuntimeExceptionHelpers
    {
        public enum RhFailFastReason
        {
            Unknown,
            InternalError,
            UnhandledException_ExceptionDispatchNotAllowed,
            UnhandledException_CallerDidNotHandle,
            ClassLibDidNotTranslateExceptionID,
            UnhandledException,
            UnhandledExceptionFromPInvoke
        }

        [RuntimeExport("GetRuntimeException")]
        public static Exception GetRuntimeException(ExceptionIDs id)
        {
            /*
            try
            {
                switch (id)
                {
                    case ExceptionIDs.OutOfMemory:
                        return PreallocatedOutOfMemoryException.Instance;
                    case ExceptionIDs.Arithmetic:
                        return new ArithmeticException();
                    case ExceptionIDs.ArrayTypeMismatch:
                        return new ArrayTypeMismatchException();
                    case ExceptionIDs.DivideByZero:
                        return new DivideByZeroException();
                    case ExceptionIDs.IndexOutOfRange:
                        return new IndexOutOfRangeException();
                    case ExceptionIDs.InvalidCast:
                        return new InvalidCastException();
                    case ExceptionIDs.Overflow:
                        return new OverflowException();
                    case ExceptionIDs.NullReference:
                        return new NullReferenceException();
                    case ExceptionIDs.DataMisaligned:
                        return new PlatformNotSupportedException();
                    default:
                        //RuntimeImports.RhpFallbackFailFast();
                        return null;
                }
              }
            catch
            {
                return null;
            }
            */

            return default; //null
        }

        //[RuntimeExport("FailFast")]
        //public static void RuntimeFailFast(RhFailFastReason reason, Exception exception, IntPtr pExAddress, IntPtr pExContext)

        //public static void FailFast(string message)

        //[RuntimeExport("AppendExceptionStackFrame")]
        //private static void AppendExceptionStackFrame(object exceptionObj, IntPtr IP, int flags)

        [RuntimeExport("OnFirstChanceException")]
        internal static void OnFirstChanceException(object e)
        {
        }

        [RuntimeExport("OnUnhandledException")]
        internal static void OnUnhandledException(object e)
        {
        }
    }
}
