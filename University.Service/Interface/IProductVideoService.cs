using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service.Interface
{
    public interface IProductVideoService
    {
        List<ProductVideos> GetProductVideoList();
    }
}
