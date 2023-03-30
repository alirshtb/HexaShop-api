using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Application.Features.CategoryFeatures.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CategoryFeatures.RequestHandlers.Queries
{
    public class GetParentCategoryListQRH : IRequestHandler<GetParentCategoryListQR, List<GetParentCategoryListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetParentCategoryListQRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetParentCategoryListDto>> Handle(GetParentCategoryListQR request, CancellationToken cancellationToken)
        {
            var parentCategories = _unitOfWork.CategoryRepository.GetListAsync();
            return new List<GetParentCategoryListDto>();
        }
    }
}
