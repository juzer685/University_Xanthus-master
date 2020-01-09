using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repository.Interface
{
   public interface IProductUserGuideRepository
    {
        List<ProductUserGuide> GetProductUserGuideList();
        List<ProductUserGuide> GetSearchUserGuides(string SearchTxt);
    }
}
