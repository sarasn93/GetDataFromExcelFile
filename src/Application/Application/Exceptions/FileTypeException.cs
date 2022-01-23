using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Exceptions
{
    public class FileTypeException : ApplicationException
    {
        public FileTypeException()
            : base($"Your File is invalid. please use .xls or .xlsx")
        {
        }
        
    }
}
