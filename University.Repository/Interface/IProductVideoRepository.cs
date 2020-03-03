using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repository.Interface
{
    public interface IProductVideoRepository
    {
      //  (List<ProductVideos>, List<CategoryUserMapping>) GetProductVideoList();
         List<ProductVideos> GetProductVideoList();
        IEnumerable<ProductVideoModel> GetUserVideosList();
    }
}
