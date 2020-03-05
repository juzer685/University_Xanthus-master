using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Areas.Admin.Models
{
    public class CardTransactionDetailsMappings
    {
        public int CTMID { get; set; }
        public Nullable<decimal> UserID { get; set; }
        public Nullable<decimal> CategoryID { get; set; }
        public Nullable<decimal> ProductID { get; set; }
        public string TransactionID { get; set; }
        public Nullable<decimal> VideoID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<decimal> CreatedBy { get; set; }
        public Nullable<decimal> DeletedBy { get; set; }
    }
}