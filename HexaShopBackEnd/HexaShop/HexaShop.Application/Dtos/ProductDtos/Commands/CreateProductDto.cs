using HexaShop.Application.Dtos.DetailDtos.Commands;
using HexaShop.Application.Dtos.ImageSourceDtos.Commands;
using HexaShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.ProductDtos.Commands
{
    public class CreateProductDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Score { get; set; }
        [MinLength(1)]
        public List<int> Categories { get; set; }

        public long Price { get; set; }

        public string MainImage { get; set; }
        public bool IsSpecial { get; set; }

        public List<CreateImageSourceDto> Images { get; set; }

        public List<CreateDetailDto> Details { get; set; }
    }
}
