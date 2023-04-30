using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common
{
    public static class ExtentionFunctions
    {

        /// <summary>
        /// order by given field and by the given direction.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="orderBy"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static IQueryable<T> PrivateOrderBy<T>(this IQueryable<T> source, string orderBy, string direction)
        {
            var fieldToOrder = typeof(T).GetProperties()
                                 .Where(p => p.Name.ToLower() == orderBy.ToLower())
                                 .FirstOrDefault();

            if(fieldToOrder == null)
            {
                return source;
            }

            var orderField = fieldToOrder.Name;
            if (orderField == null) orderField = "Id";


            if (direction.ToLower() == "asc")
            {
                source = source.OrderBy(p => orderField);
            }
            else
            {
                source = source.OrderByDescending(p => orderField);
            }

            return source;

        }
    }
}
