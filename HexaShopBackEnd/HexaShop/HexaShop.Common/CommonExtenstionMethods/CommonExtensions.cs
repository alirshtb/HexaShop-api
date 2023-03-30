using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.CommonExtenstionMethods
{
    public static class CommonExtensions
    {
        /// <summary>
        /// get enum value 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetEnumTypeName(this Type enumeration, int value)
        {
            if(!enumeration.IsEnum)
            {
                throw new Exception($"{enumeration} is not Enum Type.");
            }

            var enumValue = Enum.GetName(enumeration, value)?.ToString();

            return enumValue;
        }
    }
}
