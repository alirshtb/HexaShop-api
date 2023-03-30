using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.ImageSourceDtos.Queries
{
    public class ImageSourceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? ProductId { get; set; } // --- if product id is not null, hense this is related to a product --- //
    }
}
