using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common
{
    public static class SavePaths
    {
        /// <summary>
        /// get path for saving product images.
        /// </summary>
        /// <param name="productTitle"></param>
        /// <param name="productId"></param>
        /// <returns>save path as string.</returns>
        public static string GetSavePath(string entityPath, string productTitle, int productId)
        {
            return $"{entityPath.ToUpper()}\\{productTitle.ToUpper()}-{productId}";
        }

    }
}
