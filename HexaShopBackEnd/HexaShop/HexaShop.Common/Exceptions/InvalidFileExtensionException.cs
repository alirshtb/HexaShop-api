using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.Exceptions
{
    public class InvalidFileExtensionException : ApplicationException
    {
        public InvalidFileExtensionException(string message) : base($"{ApplicationMessages.InValidFileExtension} - {message}")
        {

        }
    }
}
