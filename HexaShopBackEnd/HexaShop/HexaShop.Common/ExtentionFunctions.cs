using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public static IQueryable<T> SystemOrderBy<T>(this IQueryable<T> source, string orderBy, string direction)
        {

            if (orderBy is null) orderBy = "Id";
            if (direction is null) direction = "asc";


            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            MemberExpression property = Expression.Property(parameter, orderBy);

            LambdaExpression lambda = Expression.Lambda(property, parameter);

            var methodName = direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);


        }


    }
}
