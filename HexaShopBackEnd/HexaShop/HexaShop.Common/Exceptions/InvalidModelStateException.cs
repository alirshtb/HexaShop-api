using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.Exceptions
{
    public class InvalidModelStateException : ApplicationException
    {
        public InvalidModelStateException(object model, string errorMessage) : base($"Error Message : {errorMessage} - Model Object : {model}")
        {

        }
    }

}
