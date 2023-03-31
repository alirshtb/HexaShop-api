using HexaShop.Application.Dtos.DetailDtos.Queries;
using HexaShop.Application.Dtos.ImageSourceDtos.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.ProductDtos.Queries
{
    public class GetProductListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public string MainImgae { get; set; }
        public long Price { get; set; }
        public int DetailsCount { get; set; }
        public int ImagesCount { get; set; }
        public int CategoriesCount { get; set; }
    }
}
