using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Data.CustomEntities
{
    public class ProductLayoutMenu
    {
        public decimal CategoryMappID { get; set; }
        public decimal CategoryUserMappID { get; set; }
        public Decimal ProductId { get; set; }
        public string ProductName { get; set; }
        public Decimal SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}
