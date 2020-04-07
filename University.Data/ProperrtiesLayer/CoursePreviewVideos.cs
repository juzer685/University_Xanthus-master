using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace University.Data
{
   public partial class CoursePreviewVideos
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
        public HttpPostedFileBase CoursePreviewVideo { get; set; }
    }
}
