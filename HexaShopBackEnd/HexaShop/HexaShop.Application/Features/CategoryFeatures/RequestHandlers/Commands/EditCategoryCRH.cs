using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Commands;
using HexaShop.Application.Dtos.CategoryDtos.Commands.Validations;
using HexaShop.Application.Features.CategoryFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CategoryFeatures.RequestHandlers.Commands
{
    public class EditCategoryCRH : IRequestHandler<EditCategoryCR, ResultDto<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditCategoryCRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultDto<int>> Handle(EditCategoryCR request, CancellationToken cancellationToken)
        {
            #region Validation

            var validator = new EditCategoryDtoValidator();

            var validationResult = await validator.ValidateAsync(request.EditCategoryDto);

            if(!validationResult.IsValid)
            {
                return new ResultDto<int>()
                {
                    IsSuccess = false,
                    Message = ApplicationMessages.InValidInformation,
                    Reason = FailureReason.InvalidModel,
                    ResultData = 0
                };
            }

            #endregion Validation


            var category = await _unitOfWork.CategoryRepository.GetAsync(request.EditCategoryDto.Id);
            if (category == null) return new ResultDto<int>()
            {
                IsSuccess = false,
                Message = validationResult.Errors.FirstOrDefault().ToString(),
                Reason = FailureReason.NotFound,
                ResultData = 0
            };

            _mapper.Map(request.EditCategoryDto, category);

            var updateResult = await _unitOfWork.CategoryRepository.UpdateAsync(category);

            return new ResultDto<int>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.CategoryEdited,
                ResultData = updateResult
            };
        }
    }
}
