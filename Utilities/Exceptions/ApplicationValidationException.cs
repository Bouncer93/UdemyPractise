using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class ApplicationValidationException : Exception
    {
        public ApplicationValidationException(string message) : base(message:message)
        {

        }
    }
}
