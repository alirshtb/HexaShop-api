using HexaShop.Application.Dtos.DetailDtos.Queries;
using HexaShop.Application.Dtos.ImageSourceDtos.Queries;

namespace HexaShop.EndPoint.Models.ViewModels.ProductController
{
    public class GetProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public string MainImgae { get; set; }
        public long Price { get; set; }
        public List<GetDetailViewModel> Details { get; set; }
        public List<GetImageSourceViewModel> Images { get; set; }
    }
}
