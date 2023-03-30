using AutoMapper;
using FluentValidation;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Commands.Validations;
using HexaShop.Application.Features.CategoryFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using HexaShop.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CategoryFeatures.RequestHandlers.Commands
{
    public class CreateCategoryCRH : IRequestHandler<CreateCategoryCR, ResultDto<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// add new category
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<int>> Handle(CreateCategoryCR request, CancellationToken cancellationToken)
        {
            var createCategoryDtoValidator = new CreateCategoryDtoValidator();
            var validationResult = createCategoryDtoValidator.Validate(request.CreateCategoryDto);

            if (!validationResult.IsValid)
            {
                return new ResultDto<int>()
                {
                    IsSuccess = false,
                    Message = validationResult.Errors.FirstOrDefault().ErrorMessage,
                    Reason = FailureReason.InvalidModel,
                    ResultData = 0
                };
            }

            // --- validate parent category --- //
            if(request.CreateCategoryDto.ParentCategoryId.HasValue)
            {
                var parentCategory = await _unitOfWork.CategoryRepository.GetAsync(request.CreateCategoryDto.ParentCategoryId.Value);
                if (parentCategory == null)
                {
                    return new ResultDto<int>()
                    {
                        IsSuccess = false,
                        Message = ApplicationMessages.ParentCategoryNotFound,
                        Reason = FailureReason.InvalidModel,
                        ResultData = 0
                    };
                }
            }


            var category = _mapper.Map<Category>(request.CreateCategoryDto);

            // --- add category --- //
            using var transaction = _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.CategoryRepository.AddAsync(category);

            await _unitOfWork.CommitAsync();

            return new ResultDto<int>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.CategoryAdded,
                ResultData = category.Id
            };
        }
    }
}
