using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class Category : BaseDomainEntity
    {
        [MinLength(3)]
        public string Name { get; set; }
        [MinLength(10)]
        public string Description { get; set; }
        public string Image { get; set; }


        #region Relations 

        public virtual ICollection<Category> ChildCategories { get; set; }
        

        public virtual Category ParentCategory { get; set; }
        public System.Nullable<int> ParentCategoryId { get; set; }


        public virtual ICollection<ProductInCategory> Products { get; set; }

        #endregion Relations
    }
}
