using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Areas.Admin.Models
{
    public class FAQViewModel
    {
        public decimal Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public Nullable<int> AssocitedID { get; set; }
       
        public virtual ICollection<FAQDocViewModel> FAQDoc { get; set; }

        
    }
}