using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.ProductDtos.Queries;
using HexaShop.Application.Features.ProductFeatures.Requests.Queries;
using HexaShop.Common;
using HexaShop.Common.Exceptions;
using MediatR;

namespace HexaShop.Application.Features.ProductFeatures.RequestHandlers.Queries
{
    public class GetProductQRH : IRequestHandler<GetProductQR, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductQRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductQR request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Images",
                "Details"
            };

            var product = await _unitOfWork.ProductRepository.GetAsync(request.ProductId, includes: includes);
            

            if(product == null)
            {
                throw new NotFoundException(ApplicationMessages.ProductNotFound);
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;

        }
    }
}
