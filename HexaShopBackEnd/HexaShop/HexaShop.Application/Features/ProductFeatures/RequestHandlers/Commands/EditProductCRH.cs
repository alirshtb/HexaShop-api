using AutoMapper;
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
    public class EditProductCRH : IRequestHandler<EditProductCR, ResultDto<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditProductCRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// edit product.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<int>> Handle(EditProductCR request, CancellationToken cancellationToken)
        {
            #region Validations 
            var validator = new EditProductDtoValidator();

            CommonStaticFunctions.ValidateModel(validator, request.EditProductData);

            #endregion Validations

            // --- get product --- //
            var includes = new List<string>()
            {
                "Images",
                "Categories",
                "Details"
            };
            var product = await _unitOfWork.ProductRepository.GetAsync(request.EditProductData.ProductId, includes: includes);
            if (product == null)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.ProductNotFound);
            }


            // --- update using transaction --- //
            using var transaction = await _unitOfWork.BeginTransactionAsync();


            #region Update product

            // --- map and update price, title, description and detials --- //
            _mapper.Map(request.EditProductData, product);

            await _unitOfWork.ProductRepository.UpdateAsync(product);


            // --- save image brach path --- //
            var saveBranch = SavePaths.GetSavePath(nameof(product), productTitle: request.EditProductData.Title, productId: product.Id);

            // --- upload new main image --- //
            var newMainImageAddress = await UploadNewMainImage(request, product, saveBranch);

            // --- uplaod new images --- //
            var newImagesAddresses = await UploadNewImages(request, product, saveBranch);


            // --- Add new categories --- //
            await AddNewCategories(product, request.EditProductData.Categories);

            await _unitOfWork.ProductRepository.UpdateAsync(product);

            #endregion Update Product


            #region Delete old image files 
            // --- delete last main image --- //
            var deleteResult = _unitOfWork.FileRepository.DeleteFile(product.MainImage);

            // --- delete old images --- //
            if (product.Images.Count() > 0)
            {
                product.Images.ToList().ForEach(img =>
                {
                    _unitOfWork.FileRepository.DeleteFile(img.Address);
                });
            }
            #endregion Delete old image files


            #region Add new Image Addresses
            // --- update main image address --- //
            product.MainImage = newMainImageAddress;

            // --- delete product all images and then add new images --- //
            await _unitOfWork.ProductRepository.DeletetImages(product.Id);
            await _unitOfWork.ProductRepository.AddImages(newImagesAddresses, product.Id);

            await _unitOfWork.ProductRepository.UpdateAsync(product);

            #endregion


            // --- commit transaction --- //
            await _unitOfWork.CommitAsync();

            return new ResultDto<int>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.ProductEdited,
                ResultData = product.Id
            };
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

        /// <summary>
        /// upload new main image
        /// </summary>
        /// <param name="request"></param>
        /// <param name="product"></param>
        /// <param name="saveBranch"></param>
        private async Task<string> UploadNewMainImage(EditProductCR request, Product product, string saveBranch)
        {
            var fileDto = new FileDto<string>()
            {
                File = request.EditProductData.MainImage,
                FileExtension = ImageExtensions.JPG,
                Name = product.Title + "-MainImage"
            };
            var mainImageAddress = UploadProductImages(fileDto, saveBranch);

            return await Task.FromResult(mainImageAddress);
        }

        /// <summary>
        /// upload new images
        /// </summary>
        /// <param name="request"></param>
        /// <param name="product"></param>
        /// <param name="saveBranch"></param>
        private async Task<List<ImageSource>> UploadNewImages(EditProductCR request, Product product, string saveBranch)
        {

            var result = new List<ImageSource>();

            if (request.EditProductData.Images.Count > 0)
            {
                foreach (var img in request.EditProductData.Images)
                {

                    var fileDto = new FileDto<string>()
                    {
                        File = img.Address,
                        FileExtension = ImageExtensions.JPG,
                        Name = img.Name
                    };

                    var imageAddress = UploadProductImages(fileDto, saveBranch);

                    result.Add(new ImageSource()
                    {
                        Name = img.Name,
                        Address = imageAddress
                    });

                };


            }

            return await Task.FromResult(result);

        }

        /// <summary>
        /// add product to new category
        /// </summary>
        /// <param name="product"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        private async Task AddNewCategories(Product product, List<int> categories)
        {
            foreach (var category in product.Categories)
            {
                await _unitOfWork.ProductRepository.RemoveFromCategory(productId: product.Id, categoryId: category.CategoryId);

            }

            foreach (var categoryId in categories)
            {
                var category = await _unitOfWork.CategoryRepository.GetAsync(categoryId, includes: new List<string>()
                {
                    "Products"
                });

                if (category == null)
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.CategoryNotFound);
                }


                await _unitOfWork.ProductRepository.AddToCategory(productId: product.Id, categoryId: categoryId);

            }

            await Task.CompletedTask;

        }

    }
}
