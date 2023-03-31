using HexaShop.Common.Dtos;
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

        /// <summary>
        /// get paginated enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetPaginatedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var result = source.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize);

            return result.AsEnumerable();
        }

    }
}
