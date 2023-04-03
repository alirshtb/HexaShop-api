namespace HexaShop.EndPoint.Models.ViewModels.ProductController
{
    public class GetProductToShowViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MainImageAddress { get; set; }
        public int Score { get; set; }
        public long Price { get; set; } 
    }
}
