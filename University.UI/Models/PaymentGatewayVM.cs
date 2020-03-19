using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace University.UI.Models
{
    public class PaymentGatewayVM
    {
        [Required(ErrorMessage = "Please enter Full Name")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Special characters and Numbers should not be entered")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Please enter Card Number")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Only Numbers are Allowed")]
        [StringLength(20, ErrorMessage = "Do not enter more than 20 Numbers")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Please enter Month")]
        public int Month { get; set; }
        [Required(ErrorMessage = "Please enter Year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter CVV")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Only Numbers are Allowed")]
        [Range(1, 999999, ErrorMessage = "Do not enter more than 6 Numbers")]
        public int CVV { get; set; }

        public string MonthAndYear
        {
            get
            {
                return Month > 9 ? Month.ToString() + Year.ToString() : "0" + Month.ToString() + Year.ToString();
            }
        }

        public string DBCardNumber
        {
            get
            {
                return "XXXXXXXXXXXX" + CardNumber.Substring(CardNumber.Length - 4);
                //return CardNumber.Substring(CardNumber.Length - 16);
            }
        }
        public decimal Amount { get; set; }
        public string CustomerFName { get; set; }
        public string ProductName { get; set; }
        public string GuidString { get; set; }

    }
}
