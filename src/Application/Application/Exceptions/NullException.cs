using System;

namespace Application.Exceptions
{
    public class NullException : ApplicationException
    {
        public NullException(string name)
            : base($"Please enter \"{name}.")
        {
        }
    }
}
