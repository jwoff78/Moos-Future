namespace System
{
    public class Exception
    {
        private string _exceptionString;

        public Exception()
        {
            
        }

        public Exception(string str)
        {
            _exceptionString = str;
        }

        //public string ExceptionString => _exceptionString;
    }
}
