using HexaShop.Common.CommonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common
{
    public static class CommonStaticFunctions
    {
        /// <summary>
        /// Retturn ResultDto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isSuccessful"></param>
        /// <param name="message"></param>
        /// <param name="resultData"></param>
        /// <param name="reason"></param>
        /// <returns>a new Instance of ResultDto with ResultData of type T filled with the given data.</returns>
        public static ResultDto<T> ReturnResult<T> (bool isSuccessful, string message, T resultData, FailureReason? reason = null)
        {
            var result = new ResultDto<T> ();

            result.IsSuccess = isSuccessful;
            result.Message = message;
            result.Reason = reason;
            result.ResultData = resultData;

            return result;
        }

        /// <summary>
        /// get lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Param"></typeparam>
        /// <param name="property"></param>
        /// <param name="leftParam"></param>
        /// <param name="rightParam"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetLambdaExpression<T, Param>(string leftParam, Param rightParam)
        {
            // This expression is lambad : e => e.Id == id
            var parameter = Expression.Parameter(typeof(T));
            var left = Expression.Property(parameter, leftParam);
            var right = Expression.Constant(rightParam);
            var equal = Expression.Equal(left, right);
            var lambdaExpression = Expression.Lambda<Func<T, bool>>(equal, parameter);

            return lambdaExpression;
        }

    }
}
