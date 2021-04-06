using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string msg)
            : base(msg)
        { }
    }
}
