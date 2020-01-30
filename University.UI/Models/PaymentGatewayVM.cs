using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Models
{
    public class PaymentGatewayVM
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CVV { get; set; }

        public string MonthAndYear
        {
            get
            {
                return Month.ToString() + Year.ToString();
            }
        }
    }
}
