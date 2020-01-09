using University.Data;
using University.Repository.Interface;
using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Repository;

namespace University.Service
{
    public class UserHomeSliderService : IUserHomeSliderService
    {
        private readonly IUserHomeSliderRepository _userHomeSliderRepository;
        public UserHomeSliderService()
        {
            _userHomeSliderRepository = new  UserHomeSliderRepository();

        }
        public List<HomeSlider> GetProductUserGuideList()
        {
            return _userHomeSliderRepository.GetProductUserGuideList();
        }

        //public List<ProductUserGuide> GetSearchUserGuides(string SearchTxt)
        //{
        //    return _productUserGuideRepository.GetSearchUserGuides(SearchTxt);
        //}
    }
}
