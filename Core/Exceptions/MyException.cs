using System;

namespace m01_labMedicine.Core.Exceptions
{
    class MyException : Exception
    {
        public int ErrorCode { get; }

        public MyException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
