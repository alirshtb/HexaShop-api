using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HexaShop.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base($"{message}")
        {

        }
    }

    public class NotFoundException<T> : ApplicationException
    {
        public NotFoundException(string message, T data) : base($"{message} - {data}")
        {

        }
    }

}
