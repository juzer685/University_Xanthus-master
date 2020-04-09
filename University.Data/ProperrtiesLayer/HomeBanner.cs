using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace University.Data
{
    public  partial class HomeBanner
    {

        string HomeBannerImagePath = WebConfigurationManager.AppSettings["HomeBannerImagePath"];
        public HttpPostedFileBase file { get; set; }
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
