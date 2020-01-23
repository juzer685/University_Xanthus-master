using University.Data;
using University.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace University.Repository
{
   public class ProductVideoRepository: IProductVideoRepository
    {
        public List<ProductVideos> GetProductVideoList()
        {
            using (var context = new UniversityEntities())
            {
                
               // int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                return context.ProductVideos.Where(x=>x.IsDeleted!=true && x.Title!=null).ToList();
            }
        }

     

    }
}
