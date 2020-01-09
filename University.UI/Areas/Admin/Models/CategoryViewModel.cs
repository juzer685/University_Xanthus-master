using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace University.UI.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        string CategoryImagePath = WebConfigurationManager.AppSettings["CategoryImagePath"];
        public Decimal Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string ImageFullPath {
            get
            {
                if (string.IsNullOrWhiteSpace(ImageURL))
                {
                    return null;
                }
                else
                {
                    return CategoryImagePath.Replace("~", "") + ImageURL;
                }
            }
        }
    }
}