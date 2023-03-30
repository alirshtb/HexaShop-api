using HexaShop.Application.Dtos.AppUserDtos.Commands;
using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.AppUserFeatures.Requests.Commands
{
    public class CreateAppUserCR : IRequest<ResultDto<int>>
    {
        public CreateAppUserDto CreateAppUserDto { get; set; }
        public string AppIdentityUserId { get; set; }
    }
}
