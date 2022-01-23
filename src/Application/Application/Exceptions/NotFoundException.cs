using System;

namespace Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name)
            : base($"Entity \"{name} was not found.")
        {
        }
    }
}
