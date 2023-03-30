using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.Dtos
{
    public class FileDto<T>
    {
        public string Name { get; set; }
        public T File { get; set; }
        public string FileExtension { get; set; } = ".jpg";
    }
}
