using System;
namespace m01_labMedicine.Extension
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
