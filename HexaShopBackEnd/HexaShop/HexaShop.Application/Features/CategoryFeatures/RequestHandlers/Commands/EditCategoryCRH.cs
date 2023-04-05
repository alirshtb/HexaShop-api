using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Commands;
using HexaShop.Application.Dtos.CategoryDtos.Commands.Validations;
using HexaShop.Application.Features.CategoryFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Common.Constants;
using HexaShop.Common.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            // --- begin transaction --- //
            using var transaction = await _unitOfWork.BeginTransactionAsync();

            var savePath = SavePaths.GetSavePath(nameof(category), category.Name, category.Id);

            var fileDto = new FileDto<string>()
            {
                Name = request.EditCategoryDto.Name,
                File = request.EditCategoryDto.Image,
                FileExtension = ImageExtensions.JPG
            };

            var imageAddress = UploadImage(fileDto, savePath);

            // --- delete old image --- //
            _unitOfWork.FileRepository.DeleteFile(category.Image);

            request.EditCategoryDto.Image = imageAddress;

            _mapper.Map(request.EditCategoryDto, category);

            var updateResult = await _unitOfWork.CategoryRepository.UpdateAsync(category);

            // --- commit trnasaction --- //
            await _unitOfWork.CommitAsync();


            return new ResultDto<int>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.CategoryEdited,
                ResultData = updateResult
            };
        }


        /// <summary>
        /// upload image and reutrn image address.
        /// </summary>
        /// <param name="fileDto"></param>
        /// <param name="brach"></param>
        /// <returns></returns>
        private string UploadImage(FileDto<string> fileDto, string brach)
        {

            var uploadProductImageResult = _unitOfWork.FileRepository.UploadImageThroughBase64(fileDto, brach);

            if (!uploadProductImageResult.IsSuccess)
            {
                uploadProductImageResult.ThrowException<string>();
            }

            return uploadProductImageResult.ResultData;

        }



    }
}
