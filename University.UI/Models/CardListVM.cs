using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Models
{
    public class CardListVM
    {
        public decimal ProductId { get; set; }
        public bool isbuy { get; set; }
        public bool isProductbuy { get; set; }
        public int VideoId { get; set; }
        public int SubCatID { get; set; }
        public string ProductName { get; set; }
        public string CustomerFName { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string BankName { get; set; }
        public string CustomerProfileId { get; set; }
        public string CustomerPaymentProfileId { get; set; }
        public string CardType { get; set; }
        public decimal Amount { get; set; }
        public int CVV { get; set; }
        public int createby { get; set; }
        public int Month { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Year { get; set; }
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


    }
}
