//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace University.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CardTransactionDetails
    {
        public decimal Id { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public string ResultCode { get; set; }
        public string Message { get; set; }
        public string AccountType { get; set; }
        public string TransId { get; set; }
        public int ResponseCode { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    
        public virtual Login_tbl Login_tbl { get; set; }
    }
}
