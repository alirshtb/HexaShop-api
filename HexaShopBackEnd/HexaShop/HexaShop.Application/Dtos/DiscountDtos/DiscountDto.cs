using HexaShop.Application.Dtos.ProductDtos.Queries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.DiscountDtos
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
