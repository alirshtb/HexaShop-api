using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.ProductDtos.Queries
{
    public class GetProductToShowDto
    {
        public int Id { get; set; }
        public string MainImageAddress { get; set; }
        public string Title { get; set; }
        public long Price { get; set; }
        public int Score { get; set; } 
    }
}
