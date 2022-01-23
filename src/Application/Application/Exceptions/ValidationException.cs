using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException()
            : base("One or more validation failures have occurred. please check your excel file and remove header and column names.")
        {
        }
    }
}
