using HexaShop.Common.CommonDtos;
using HexaShop.Common.Exceptions;
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
        /// throw exception accounrding to the ResultDto.Reason
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultDto"></param>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="InvalidModelStateException"></exception>
        public static void ThrowException<T>(this ResultDto<T> resultDto)
        {
            if (resultDto.IsSuccess)
            {
                return;
            }

            var reason = resultDto.Reason;

            switch (reason)
            {
                case FailureReason.NotFound:
                    throw new NotFoundException(resultDto.Message);
                case FailureReason.InvalidModel:
                    throw new InvalidModelStateException(resultDto.ResultData, resultDto.Message);
                case FailureReason.UnSuccessful:
                    throw new UnSuccessfulException(resultDto.Message);
                case FailureReason.InvalidFileExtension:
                    throw new InvalidFileExtensionException(resultDto.Message);
            }
        }
    }
}
