using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service.Interface
{
    public interface IProductUserGuideService
    {
        List<ProductUserGuide> GetProductUserGuideList();
        List<ProductUserGuide> GetSearchUserGuides(string SearchTxt);
    }
}
