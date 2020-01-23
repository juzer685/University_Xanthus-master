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
   public class ProductUserGuideRepository: IProductUserGuideRepository
    {
        public IEnumerable<ProductUserGuide> GetUserGuideList()
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
                           join pu in context.ProductUserGuide.Where(y => y.IsDeleted != true)
                           on p.Id equals pu.ProductId
                           orderby pu.CreatedDate
                           select new
                           {
                               pu.Id,
                               pu.Title,
                               pu.Description,
                               pu.ProductId,
                               pu.ImageURL 
                           }

                           ).ToList();
                List<ProductUserGuide> prodguide = new List<ProductUserGuide>();
                foreach (var prodUserGuide in res)
                {

                    prodguide.Add(new ProductUserGuide
                    {
                        Id = prodUserGuide.Id,
                        Title = prodUserGuide.Title,
                        Description = prodUserGuide.Description,
                        ImageURL = prodUserGuide.ImageURL,
                        ProductId= prodUserGuide.ProductId

                    });
                    
                }
                return prodguide;
            }
        }

        public List<ProductUserGuide> GetProductUserGuideList()
        {
            using (var context = new UniversityEntities())
            {
                //int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                       
                return context.ProductUserGuide.Where(y => y.IsDeleted != true && y.Title != null && y.Product.SubCategoryMaster.IsDeleted == false ).ToList();
             
            }
        }

        public List<ProductUserGuide> GetSearchUserGuides(string SearchTxt)
        {
            using (var context = new UniversityEntities())
            {
                return context.ProductUserGuide.Where(y => y.Title.Contains(SearchTxt)).ToList();
            }
        }
    }
}
