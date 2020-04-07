﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Data.CustomEntities
{
    public class ProductEntity
    {
        //public decimal subcatid { get; set; }
        public Decimal VideoIds { get; set; }
        public string TransactionId { get; set; }
        public decimal? VideoRateSum { get; set; }
        public Decimal Id { get; set; }
        public int? AssocitedCustID { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string ImageALT { get; set; }
        public string Description { get; set; }
        public Nullable<Decimal> SubCategoryId { get; set; }
        //public Nullable<Decimal> CategoryId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
        public virtual CategoryMaster CategoryMaster { get; set; }
        public virtual SubCategoryMaster SubCategoryMaster { get; set; }
        public virtual ProductUserGuide ProductUserGuide { get; set; }
        public virtual List<ProductVideos> ProductVideos { get; set; }
        public virtual List<CoursePreviewVideos> CoursePrviewVideos { get; set; }

        //public virtual List<ProductVideoModel> ProductVideos { get; set; }
        public virtual List<CardTransactionDeatilsMapping> ProductVideosTranIDs { get; set; }
        public virtual ProductSpec ProductSpec { get; set; }
        public virtual ICollection<ProductFAQs> ProductFAQs { get; set; }
        public virtual ICollection<ProductDocuments> ProductDocuments { get; set; }
        public virtual ICollection<CategoryUserMapping> Categorymapp { get; set; }
        public virtual ICollection<CardTransactionDetails> CardTransactionDetails { get; set; }
        //public virtual ICollection<CardTransactionDetailsMappings> CardTransactionDetailsMappings  { get; set; }
        
    }
}
