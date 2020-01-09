using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Core
{
    public class ProductBusinessModel
    {
        public Decimal Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public Decimal? SubCategoryId { get; set; }
        public Nullable<Decimal> CategoryId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<Decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<Decimal> DeletedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<Decimal> UpdatedBy { get; set; }
    }
}
