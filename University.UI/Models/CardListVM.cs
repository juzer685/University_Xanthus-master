using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Models
{
    public class CardListVM
    {
        public string CardNumber { get; set; }
        public string BankName { get; set; }
        public string CustomerProfileId { get; set; }
        public string CustomerPaymentProfileId { get; set; }
        public string CardType { get; set; }
        public decimal Amount { get; set; }
    }
}
