using HexaShop.Application.Dtos.CategoryDtos.Queries;

namespace HexaShop.EndPoint.Models.ViewModels.CategoryController
{
    public class GetCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GetCategoryViewModel> Childs { get; set; }
        //public int ParentId { get; set; }
    }
}
