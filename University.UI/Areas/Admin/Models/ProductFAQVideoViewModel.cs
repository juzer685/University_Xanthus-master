using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace University.UI.Areas.Admin.Models
{
    public class ProductFAQVideoViewModel
    {
        string ProductImagePath = WebConfigurationManager.AppSettings["ProductImagePath"];
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ImageURL))
                {
                    return null;
                }
                else
                {
                    return ProductImagePath.Replace("~", "") + ImageURL;
                }
            }
        }
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
        public int Id { get; set; }
        public int AssocitedID { get; set; }
        public Decimal ProductFAQsId { get; set; }
        public string ImageURL { get; set; }
        public string VideoURL { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public HttpPostedFileBase FAQVideoImg { get; set; }
        public HttpPostedFileBase FAQVideo { get; set; }
    }
}