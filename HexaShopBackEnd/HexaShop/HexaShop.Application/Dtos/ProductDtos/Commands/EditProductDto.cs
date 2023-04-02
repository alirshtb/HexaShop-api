using HexaShop.Application.Dtos.DetailDtos.Commands;
using HexaShop.Application.Dtos.ImageSourceDtos.Commands;

namespace HexaShop.Application.Dtos.ProductDtos.Commands
{
    public class EditProductDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string MainImage { get; set; }
        public List<CreateDetailDto> Details { get; set; }
        public List<CreateImageSourceDto> Images { get; set; }
        public List<int> Categories { get; set; }
    }
}
