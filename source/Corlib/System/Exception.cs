namespace System
{
    public abstract class Exception
    {
        private string _exceptionString;

        public Exception()
        {
            
        }

        public Exception(string str)
        {
            _exceptionString = str;
        }
    }
}
