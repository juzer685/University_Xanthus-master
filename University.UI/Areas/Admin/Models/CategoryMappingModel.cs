using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using University.Data;

namespace University.UI.Areas.Admin.Models
{
    public class CategoryMappingModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> AdminID { get; set; }
        //public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        //CategoryId
        public decimal CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Login_tbl> Logintbllst { get; set; }
        public List<SubCategoryMaster> SubCategoryMasterlst { get; set; }
        public List<CategoryUserMapping> CategoryUserMapping { get; set; }
       
    }
}