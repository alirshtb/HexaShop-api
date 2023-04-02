using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaShop.EndPoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> RemoveImages(int id)
        {
            await _unitOfWork.ProductRepository.DeleteProductImages(id);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> AddImageToProduct(int id)
        {
            await _unitOfWork.ProductRepository.AddImagesToProduct(images: new List<ImageSource>()
            {
                new ImageSource()
                {
                    Name = "image1",
                    Address = "address1"
                },
                new ImageSource()
                {
                    Name = "image2",
                    Address = "address2"
                }
            }, productId: id);

            return Ok();
        }


    }
}
