using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.DiscountDtos.Validations;
using HexaShop.Application.Features.DiscountFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using HexaShop.Common.CommonExtenstionMethods;
using MediatR;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.DiscountFeatures.RequestHandlers.Commands
{
    public class EditDiscountCRH : IRequestHandler<EditDiscountCR, ResultDto<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditDiscountCRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultDto<int>> Handle(EditDiscountCR request, CancellationToken cancellationToken)
        {
            #region Validation

            var validator = new EditDiscountDtoValidator();
            CommonStaticFunctions.ValidateModel(validator, request.EditDiscountDto);

            var discount = await _unitOfWork.DiscountRepository.GetAsync(request.EditDiscountDto.DiscountId);
            if (discount == null)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.DiscountNotFound);
            }

            // --- if this percent is created throw exception --- //
            if (_unitOfWork.DiscountRepository.IsDuplicate(request.EditDiscountDto.Percent))
            {
                throw new Exception(ApplicationMessages.DuplicateDiscount);
            }
            #endregion

            _mapper.Map(request.EditDiscountDto, discount);

            await _unitOfWork.DiscountRepository.UpdateAsync(discount);
            
            return new ResultDto<int>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.DiscountEdited,
                ResultData = discount.Id
            };
        }
    }
}
