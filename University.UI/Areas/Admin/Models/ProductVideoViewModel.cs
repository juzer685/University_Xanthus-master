using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace University.UI.Areas.Admin.Models
{
    public class ProductVideoViewModel
    {
        string ProductImagePath = WebConfigurationManager.AppSettings["ProductImagePath"];
        public string TransactionId { get; set; }
        public string VideoFullPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(VideoURL))
                {
                    return null;
                }
                else
                {
                    return ProductImagePath.Replace("~", "") + VideoURL;
                }
            }
        }
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(VideoURL))
                {
                    return null;
                }
                else
                {
                    return ProductImagePath.Replace("~", "") + ThumbnailURL;
                }
            }
        }
        public int? cateuserid { get; set; }
        public Decimal Id { get; set; }
       // [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string Title { get; set; }
        //[StringLength(100, ErrorMessage = "Do not enter more than 100 characters")]
        public string Decription { get; set; }
        public string VideoURL { get; set; }
        public string ThumbnailURL { get; set; }
        public Decimal ProductId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public HttpPostedFileBase ProductVideoImg { get; set; }
        public HttpPostedFileBase ProductVideo { get; set; }
        public Decimal AssocitedCustID { get; set; }
        public decimal VideoRate { get; set; }
        public bool IsPaid { get; set; }
        public Decimal SubCatID { get; set; }


    }
}