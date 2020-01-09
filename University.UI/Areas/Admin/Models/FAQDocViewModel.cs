using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Areas.Admin.Models
{
    public class FAQDocViewModel
    {
        public Decimal Id { get; set; }
        public Decimal FAQId { get; set; }
        public string ImageURL { get; set; }
        public string VideoURL { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        //public Decimal AssocitedID { get; set; }
        public virtual FAQViewModel FAQ { get; set; }
    }
}