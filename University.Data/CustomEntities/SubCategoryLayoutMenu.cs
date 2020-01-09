using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Data.CustomEntities
{
    public class SubCategoryLayoutMenu
    {
        public Decimal Id { get; set; }
        public string Name { get; set; }
        public List<ProductLayoutMenu> Products { get; set; }
    }
}
