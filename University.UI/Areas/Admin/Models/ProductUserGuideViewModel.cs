using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace University.UI.Areas.Admin.Models
{
    public class ProductUserGuideViewModel
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
        public Decimal Id { get; set; }
        public Decimal AssocitedCustID { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public Nullable<Decimal> ProductId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public HttpPostedFileBase Guidefile { get; set; }
        public string DocumentURL { get; set; }
        public string ImageALT { get; set; }
        public string CheckImage
        {
            get
            {
                if (string.IsNullOrEmpty(ImageURL))
                {
                    return "/images/NoImageAvailable.jpg";
                }
                else
                {
                    return ImageFullPath;
                }
            }
        }
    }
}