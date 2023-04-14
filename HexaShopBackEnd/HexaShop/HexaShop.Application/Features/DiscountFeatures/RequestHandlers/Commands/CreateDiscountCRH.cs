using AutoMapper;
using FluentValidation;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.DiscountDtos.Validations;
using HexaShop.Application.Features.DiscountFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.DiscountFeatures.RequestHandlers.Commands
{
    public class CreateDiscountCRH : IRequestHandler<CreateDiscountCR, ResultDto<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDiscountCRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultDto<int>> Handle(CreateDiscountCR request, CancellationToken cancellationToken)
        {
            #region Validations 
            var validator = new CreateDiscountDtoValidator();
            CommonStaticFunctions.ValidateModel(validator, request.CreateDiscountDto);

            // --- if this percent is created throw exception --- //
            if(_unitOfWork.DiscountRepository.IsDuplicate(request.CreateDiscountDto.Percent))
            {
                throw new Exception(ApplicationMessages.DuplicateDiscount);
            }

            #endregion 

            var discount = new Domain.Discount()
            {
                Title = request.CreateDiscountDto.Title,
                Description = request.CreateDiscountDto.Description,
                Percent = request.CreateDiscountDto.Percent,
                IsActive = true,
                IsDeleted = false
            };

            await _unitOfWork.DiscountRepository.AddAsync(discount);


            return new ResultDto<int>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.DiscountCreated,
                ResultData = discount.Id
            };
        }
    }
}
