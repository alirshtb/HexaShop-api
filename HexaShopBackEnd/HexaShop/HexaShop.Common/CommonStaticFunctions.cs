using FluentValidation;
using HexaShop.Common.CommonDtos;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// validate model
        /// </summary>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="validator"></param>
        /// <param name="model"></param>
        /// <exception cref="InvalidModelException"></exception>
        public static void ValidateModel<TValidator, TModel>(TValidator validator, TModel model)
        {
            var modelValidator = validator as AbstractValidator<TModel>;

            var validationResult = modelValidator.Validate(model);

            if(!validationResult.IsValid)
            {
                ExceptionHelpers.ThrowException(validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(), model);
            }

        }
        
    }
}
