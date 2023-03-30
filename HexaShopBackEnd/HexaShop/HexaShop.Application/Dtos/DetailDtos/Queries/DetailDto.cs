using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.DetailDtos.Queries
{
    public class DetailDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int? ProductId { get; set; }
    }
}
