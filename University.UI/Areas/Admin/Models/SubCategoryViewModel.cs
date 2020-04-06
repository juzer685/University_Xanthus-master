using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.ComponentModel.DataAnnotations;

namespace University.UI.Areas.Admin.Models
{
    public class SubCategoryViewModel
    {
        string SubCategoryImagePath = WebConfigurationManager.AppSettings["SubCategoryImagePath"];
        public Decimal Id { get; set; }
        // public Decimal CategoryId { get; set; }
        //[StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
       // [MaxLength(50)]
        public string Name { get; set; }
       
        public HttpPostedFileBase file { get; set; }
        public string ImageURL { get; set; }
        public string ImageALT { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public virtual CategoryMaster CategoryMaster { get; set; }
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
                    return SubCategoryImagePath.Replace("~", "") + ImageURL;
                }
            }
        }
        public Decimal AssocitedCustID { get; set; }
    }
}