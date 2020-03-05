using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.Data
{
    public class ProductVideoModel
    {
        public decimal Id { get; set; }
        public string Title { get; set; }
        public string Decription { get; set; }
        public string VideoURL { get; set; }
        public Decimal ProductId { get; set; }
        public bool IsPaid { get; set; }
        public decimal VideoRate { get; set; }
        public Decimal SubcatId { get; set; }
        public string TransactionId { get; set; }


    }
}