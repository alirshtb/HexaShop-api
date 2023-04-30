namespace HexaShop.EndPoint.Models.ViewModels.DiscountController
{
    public class DiscountViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProductsCount { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
    }
}
