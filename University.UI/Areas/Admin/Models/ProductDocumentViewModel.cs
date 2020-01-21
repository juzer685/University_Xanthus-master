using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace University.UI.Areas.Admin.Models
{
    public class ProductDocumentViewModel
    {
        string ProductImagePath = WebConfigurationManager.AppSettings["ProductImagePath"];
        public string DocumentFullPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DocumentURL))
                {
                    return null;
                }
                else
                {
                    return ProductImagePath.Replace("~", "") + DocumentURL;
                }
            }
        }

        public Decimal Id { get; set; }
        public Decimal AssocitedCustID { get; set; }
        
        public string Title { get; set; }
        public string Decription { get; set; }
        public string DocumentURL { get; set; }
        public Decimal ProductId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public HttpPostedFileBase ProductDocumentFile { get; set; }
        public string DocumentDisplayName { get; set; }
    }
}