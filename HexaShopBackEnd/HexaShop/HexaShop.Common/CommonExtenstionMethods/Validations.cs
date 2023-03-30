using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.CommonExtenstionMethods
{
    public static class Validations
    {

        /// <summary>
        /// Validate enum Type values.
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="type"></param>
        /// <returns>true if type is defined in enumeration.</returns>
        public static bool ValidateEnumTypeValue(Type enumeration, int type)
        {
            if(!enumeration.IsEnum)
            {
                return false;
            }

            var isValid = true;
            if (!Enum.IsDefined(enumeration, type))
            {
                return !isValid;
            }
            return isValid;
        }
    }
}
