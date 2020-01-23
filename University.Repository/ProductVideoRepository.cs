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
    public class ProductVideoRepository : IProductVideoRepository
    {
        public List<ProductVideos> GetProductVideoList()
        {
            using (var context = new UniversityEntities())
            {

                // int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                return context.ProductVideos.Where(x => x.IsDeleted != true && x.Title != null).ToList();
            }
        }
        public IEnumerable<ProductVideos> GetUserVideosList()
        {
            using (var context = new UniversityEntities())
            {
                int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);

                var res = (from l in context.Login_tbl.Where(y => y.IsDeleted != true && y.ID == UserID)
                           join cm in context.CategoryUserMapping.Where(y => y.IsDeleted != true && y.UserID == UserID)
                           on l.ID equals cm.UserID
                           join c in context.SubCategoryMaster.Where(y => y.IsDeleted != true)
                           on cm.CategoryID equals c.Id
                           join p in context.Product.Where(y => y.IsDeleted != true)
                           on c.Id equals p.SubCategoryId
                           join pv in context.ProductVideos.Where(y => y.IsDeleted != true)
                           on p.Id equals pv.ProductId
                           orderby pv.CreatedDate
                           select new
                           {
                               pv.Id,
                               pv.Title,
                               pv.Decription,
                               pv.ProductId,
                               pv.VideoURL
                           }

                           ).ToList();
                List<ProductVideos> productvideo = new List<ProductVideos>();
                foreach (var prodvideo in res)
                {
                    productvideo.Add(new ProductVideos
                    {
                        Id = prodvideo.Id,
                        Title=prodvideo.Title,
                        Decription=prodvideo.Decription,
                        VideoURL=prodvideo.VideoURL
                        
                    });
                    //var title = prodvideo.Title;
                    //var des = prodvideo.Decription;
                    //var ProductId = prodvideo.ProductId;
                    //var VideoURL = prodvideo.VideoURL;
                }
                return productvideo;
            }
        }



    }
}
