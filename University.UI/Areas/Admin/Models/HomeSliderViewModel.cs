using University.Data;
using University.Data.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.ComponentModel.DataAnnotations;

namespace University.UI.Areas.Admin.Models
{ 
    public class HomeSliderViewModel
    {
        string HomeSliderImagePath = WebConfigurationManager.AppSettings["HomeSliderImagePath"];
        public int Id { get; set; }
        public string Link { get; set; }
        [StringLength(100, ErrorMessage = "Do not enter more than 100 characters")]
        public string TextDescription { get; set; }
        public string ImageURL { get; set; }
        public string ImageALT { get; set; }
        public Decimal ProductId { get; set; }
        public virtual Product Product { get; set; }
        public IEnumerable<ProductDropdownListViewModel> Products { get; set; }
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
                    return HomeSliderImagePath.Replace("~", "") + ImageURL;
                }
            }
        }
        public int AssocitedCustID { get; set; }

        public string Title { get; set; }
        

    }
}