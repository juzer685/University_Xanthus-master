using University.Data;
using University.Data.CustomEntities;
using University.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Models
{
    public class HomePageViewModel
    {
        public int AssocitedID { get; set; }
        public IEnumerable<HomeSliderViewModel> HomeBanners { get; set; }
        public HomeBanner BannerImage { get; set; }
        public IEnumerable<FAQViewModel> ProductFAQs { get; set; }
        public IEnumerable<ProductEntity> Products { get; set; }
        public List<CategoryMaster> Categories { get; set; }
        public IEnumerable<SubCategoryMaster> HomeBannersted { get; set; }
        public List<SubCategoryMaster> SubCategories { get; set; }
        
        public string Feedback { get; set; }
        public string LastViewedProduct { get; set; }

    }
}