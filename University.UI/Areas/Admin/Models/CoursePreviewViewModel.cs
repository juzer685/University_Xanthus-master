using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using University.Data;

namespace University.UI.Areas.Admin.Models
{
    public class CoursePreviewViewModel
    {
        string ProductImagePath = WebConfigurationManager.AppSettings["ProductImagePath"];
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
        public decimal PreviewID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }
        public Nullable<decimal> ProductID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<decimal> UpdatedBy { get; set; }
        public Nullable<int> AssocitedCustID { get; set; }

        public virtual Product Product { get; set; }
        public HttpPostedFileBase CoursePreviewVideo { get; set; }

    }
}