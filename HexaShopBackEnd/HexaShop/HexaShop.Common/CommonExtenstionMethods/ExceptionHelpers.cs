using HexaShop.Common.CommonDtos;
using HexaShop.Common.CustomeExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.CommonExtenstionMethods
{
    public static class ExceptionHelpers
    {

        /// <summary>
        /// throw normal exception.
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="Exception"></exception>
        public static void ThrowException(string message)
        {
            throw new Exception(message);
        }

        /// <summary>
        /// throw invalid model exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="model"></param>
        /// <exception cref="InvalidModelException"></exception>
        public static void ThrowException<T>(string message, T model)
        {
            throw new InvalidModelException($"Message = {message} - Model = {model}");
        }

    }
}
