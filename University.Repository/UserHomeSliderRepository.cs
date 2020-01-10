using University.Data;
using University.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repository
{
   public class UserHomeSliderRepository: IUserHomeSliderRepository
    {
        

        public List<HomeSlider> GetProductUserGuideList()
        {
            using (var context = new UniversityEntities())
            {
                return context.HomeSlider.Where(y=>y.IsDeleted != true ).ToList();
            }
        }

        //public List<ProductUserGuide> GetSearchUserGuides(string SearchTxt)
        //{
        //    using (var context = new UniversityEntities())
        //    {
        //        return context.ProductUserGuide.Where(y => y.Title.Contains(SearchTxt)).ToList();
        //    }
        //}
    }
}
