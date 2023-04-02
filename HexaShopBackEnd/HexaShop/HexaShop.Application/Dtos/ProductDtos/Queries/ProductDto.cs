using HexaShop.Application.Dtos.DetailDtos.Queries;
using HexaShop.Application.Dtos.ImageSourceDtos.Queries;

namespace HexaShop.Application.Dtos.ProductDtos.Queries
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public string MainImage { get; set; }
        public long Price { get; set; }
        public List<DetailDto> Details { get; set; }
        public List<ImageSourceDto> Images { get; set; }
    }
}
