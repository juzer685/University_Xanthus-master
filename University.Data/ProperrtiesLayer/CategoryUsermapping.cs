using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Data
{
    public partial class CategoryUserMapping
    {
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string CategoryName { get; set; }
    }
}
