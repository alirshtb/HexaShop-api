﻿using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.ProductDtos.Commands.Validations;
using HexaShop.Application.Features.ProductFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Common.Constants;
using HexaShop.Common.Dtos;
using HexaShop.Domain;
using MediatR;

namespace HexaShop.Application.Features.ProductFeatures.RequestHandlers.Commands
{
    public class CreateProductCRH : IRequestHandler<CreateProductCR, ResultDto<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCRH(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultDto<int>> Handle(CreateProductCR request, CancellationToken cancellationToken)
        {

            #region Validations

            // --- validate model --- //
            var createProductValidator = new CreateProductDtoValidator();

            CommonStaticFunctions.ValidateModel(createProductValidator, request.CreateProductDto);

            var discount = await _unitOfWork.DiscountRepository.GetAsync(request.CreateProductDto.DiscountId);
            if (discount == null)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.DiscountNotFound);
            }

            #endregion

            var product = _mapper.Map<Product>(request.CreateProductDto);

            using var transaction = _unitOfWork.BeginTransaction();

            try
            {

                // --- create product and return new prduct id --- //
                _unitOfWork.ProductRepository.Add(product);

                // --- save image brach path --- //
                var saveBranch = SavePaths.GetSavePath("Product", productTitle: request.CreateProductDto.Title, productId: product.Id);


                // --- Upload product main image and get image source url and set to product before creating new product --- //
                #region upload main image and update product 
                var fileDto = new FileDto<string>()
                {
                    File = request.CreateProductDto.MainImage,
                    FileExtension = ImageExtensions.JPG,
                    Name = product.Title + "-MainImage"
                };
                var mainImageAddress = UploadProductImages(fileDto, saveBranch);

                product.MainImage = mainImageAddress;



                #endregion

                // --- upload images and update images address --- //
                #region Upload Images 
                if (request.CreateProductDto.Images.Count > 0)
                {
                    request.CreateProductDto.Images.ForEach(img =>
                    {
                        var fileDto = new FileDto<string>()
                        {
                            File = img.Address,
                            FileExtension = ImageExtensions.JPG,
                            Name = img.Name
                        };

                        var imageAddress = UploadProductImages(fileDto, saveBranch);

                        product.Images.Where(i => i.Address == img.Address).ToList().ForEach(imgFile =>
                        {
                            imgFile.Address = imageAddress;
                        });

                    });
                }
                #endregion Upload Images

                // --- add product to category --- //
                #region Add Categories 

                foreach (var categoryId in request.CreateProductDto.Categories)
                {
                    var category = await _unitOfWork.CategoryRepository.GetAsync(categoryId, includes: new List<string>()
                {
                    "Products"
                });

                    if (category == null)
                    {
                        ExceptionHelpers.ThrowException(ApplicationMessages.CategoryNotFound);
                    }

                    var productInCategories = new ProductInCategory()
                    {
                        ProductId = product.Id,
                        CategoryId = category.Id
                    };

                    category.Products.Add(productInCategories);
                }

                #endregion


                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Commit();


                return new ResultDto<int>()
                {
                    IsSuccess = true,
                    Message = ApplicationMessages.ProductAdded,
                    ResultData = product.Id
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

        }

        /// <summary>
        /// upload images.
        /// </summary>
        /// <param name="images"></param>
        /// <param name="productId"></param>
        /// <param name="brach"></param>
        /// <returns></returns>
        private string UploadProductImages(FileDto<string> fileDto, string brach)
        {

            var uploadProductImageResult = _unitOfWork.FileRepository.UploadImageThroughBase64(fileDto, brach);

            if (!uploadProductImageResult.IsSuccess)
            {
                ExceptionHelpers.ThrowException(uploadProductImageResult.Message);
            }

            return uploadProductImageResult.ResultData;

        }


    }
}
