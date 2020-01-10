using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace University.UI.Areas.Admin.Models
{
    public class HomeBannerViewModel
    {
        string HomeBannerImagePath = WebConfigurationManager.AppSettings["HomeBannerImagePath"];
        public Decimal Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LinkTo { get; set; }
        public string ImageALT { get; set; }
        public string ImageURL { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public Decimal AssocitedID { get; set; }
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
                    return HomeBannerImagePath.Replace("~", "") + ImageURL;
                }
            }
        }
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