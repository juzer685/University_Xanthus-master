using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace University.UI.Areas.Admin.Models
{
    public class ProductFaqViewModel
    {
        public ProductFaqViewModel()
        {
            ProductFAQVideoList = new List<ProductFAQVideoViewModel>();
        }
        public Decimal Id { get; set; }
        public Decimal AssocitedCustID { get; set; }
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string Question { get; set; }
        [StringLength(150, ErrorMessage = "Do not enter more than 150 characters")]
        public string Answer { get; set; }
        public Decimal ProductId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public virtual List<ProductFAQVideoViewModel> ProductFAQVideoList { get; set; }
    }
}