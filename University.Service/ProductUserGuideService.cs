using University.Data;
using University.Repository.Interface;
using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service
{
    public class ProductUserGuideService: IProductUserGuideService
    {
        private readonly IProductUserGuideRepository _productUserGuideRepository;
        public ProductUserGuideService(IProductUserGuideRepository productUserGuideRepository)
        {
            _productUserGuideRepository = productUserGuideRepository;

        }
        public List<ProductUserGuide> GetProductUserGuideList()
        {
            return _productUserGuideRepository.GetProductUserGuideList();
        }

        public List<ProductUserGuide> GetSearchUserGuides(string SearchTxt)
        {
            return _productUserGuideRepository.GetSearchUserGuides(SearchTxt);
        }
    }
}
