using System;

namespace CqrsSample.Api.Exceptions
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string message)
            : base(message)
        {
        }
    }
}
