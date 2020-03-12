using University.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using University.Data.CustomEntities;

namespace University.UI.Areas.Admin.Models
{
    public class ProductViewModel
    {
        string ProductImagePath = WebConfigurationManager.AppSettings["ProductImagePath"];
        //public string TransactionId { get; set; }
        public ProductViewModel()
        {
            this.ProductFAQs = new List<ProductFaqViewModel>();
            this.ProductFeedbacks = new HashSet<ProductFeedback>();
            this.ProductSpec = new ProductSpec();
          //  this.ProductUserGuides = new HashSet<ProductUserGuide>();
            this.ProductVideos = new List<ProductVideoViewModel>();
            this.ProductUserGuide = new ProductUserGuideViewModel();
        }

        public ProductForm ProductForm { get; set; }
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
        public decimal sumvideorate { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string subcat { get; set; }
        public string ImageALT { get; set; }
        public string Description { get; set; }
        public string VideosIDs { get; set; }
        public Nullable<Decimal> SubCategoryId { get; set; }
        //public Nullable<Decimal> CategoryId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public virtual ProductUserGuideViewModel ProductUserGuide { get; set; }

        public virtual SubCategoryMaster SubCategoryMaster { get; set; }

        //public virtual List<ProductFaqViewModel> ProductFAQs { get; set; }
        public virtual List<ProductFaqViewModel> ProductFAQs { get; set; }

        public virtual ICollection<ProductFeedback> ProductFeedbacks { get; set; }

        public virtual ProductSpec ProductSpec { get; set; }

        //public virtual ICollection<ProductUserGuide> ProductUserGuides { get; set; }

        public virtual List<ProductVideoViewModel> ProductVideos { get; set; }

        public virtual List<CardTransactionDetailsMappings> ProductVideosTranIDs { get; set; }
        public virtual CategoryMaster CategoryMaster { get; set; }
        public virtual List<ProductDocumentViewModel> ProductDocuments { get; set; }

        public int AssocitedCustID { get; set; }
       
        public string CheckImage { 
            get {
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


    public enum ProductForm
    {
        BasicDetails,
        UserGuide
    }
}