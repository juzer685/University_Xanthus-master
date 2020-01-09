using University.Data;
using University.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repository
{
   public class ProductUserGuideRepository: IProductUserGuideRepository
    {
        public List<ProductUserGuide> GetProductUserGuideList()
        {
            using (var context = new UniversityEntities())
            {
                return context.ProductUserGuide.Where(y=>y.IsDeleted != true && y.Title!=null && y.Product.SubCategoryMaster.IsDeleted == false).ToList();
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
