using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using MediatR;
using HexaShop.Domain;
using HexaShop.Application.Features.AppUserFeatures.Requests.Commands;
using HexaShop.Application.Dtos.AppUserDtos.Commands.Validations;

namespace HexaShop.Application.Features.AppUserFeatures.RequestHandlers.Commands
{
    public class CreateAppUserCRH : IRequestHandler<CreateAppUserCR, ResultDto<int>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAppUserCRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultDto<int>> Handle(CreateAppUserCR request, CancellationToken cancellationToken)
        {
            var createValidator = new CreateAppUserDtoValidator();

            CommonStaticFunctions.ValidateModel(createValidator, request.CreateAppUserDto);

            var appUser = _mapper.Map<AppUser>(request.CreateAppUserDto);
            appUser.AppIdentityUserId = request.AppIdentityUserId;

            await _unitOfWork.AppUserRepository.AddAsync(appUser);

            return new ResultDto<int>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.UserCreatedSuccessfuly,
                ResultData = appUser.Id
            };

        }
    }
}
