using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Features.ProductFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using MediatR;

namespace HexaShop.Application.Features.ProductFeatures.RequestHandlers.Commands
{
    public class ChangeProductActivityCRH : IRequestHandler<ChangeProductActivityCR, ResultDto<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeProductActivityCRH(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// change product activity.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<int>> Handle(ChangeProductActivityCR request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(request.ProductId);

            if (product == null)
            {
                return new ResultDto<int>()
                {
                    IsSuccess = false,
                    Message = ApplicationMessages.ProductNotFound,
                    Reason = FailureReason.NotFound,
                    ResultData = 0
                };
            }

            product.IsActive = !product.IsActive;

            await _unitOfWork.ProductRepository.UpdateAsync(product);

            var activityMessage = product.IsActive ? "فعال" : "غیر فعال";

            return new ResultDto<int>()
            {
                IsSuccess = true,
                Message = string.Format(ApplicationMessages.ChangeActivity, "محصول", activityMessage),
                ResultData = product.Id
            };
        }
    }
}
