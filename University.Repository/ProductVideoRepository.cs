using University.Data;
using University.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repository
{
   public class ProductVideoRepository: IProductVideoRepository
    {
        public List<ProductVideos> GetProductVideoList()
        {
            using (var context = new UniversityEntities())
            {
                return context.ProductVideos.Where(x=>x.IsDeleted!=true && x.Title!=null).ToList();
            }
        }

     

    }
}
