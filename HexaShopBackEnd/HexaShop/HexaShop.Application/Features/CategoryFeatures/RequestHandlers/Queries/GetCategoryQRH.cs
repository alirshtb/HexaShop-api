using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Application.Features.CategoryFeatures.Requests.Queries;
using HexaShop.Common;
using HexaShop.Common.CommonExtenstionMethods;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CategoryFeatures.RequestHandlers.Queries
{
    public class GetCategoryQRH : IRequestHandler<GetCategoryQR, CategoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryQRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryQR request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(request.Id, includes: new List<string>()
            {
                "ChildCategories"
            });

            if(category == null)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.CategoryNotFound);
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;

        }
    }
}
