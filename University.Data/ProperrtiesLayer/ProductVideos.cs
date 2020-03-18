using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Data
{
    public partial class ProductVideos
    {
        public Decimal catid { get; set; }
        public decimal? VideoRateSum { get; set; }
        public string subcategoryname { get; set; }
        public bool IsPaid { get; set; }
        public bool ispaidvideo { get; set; }
        public string TransactionID { get; set; }
    }
}
