using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.DiscountDtos
{
    public class EditDiscountDto
    {
        public int DiscountId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Percent { get; set; }
    }
}
