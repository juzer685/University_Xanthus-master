using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Data.CustomEntities;
using University.Repository;
using University.Repository.Interface;
using University.Service.Interface;

namespace University.Service
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _sliderRepository;
        public HomeService(IHomeRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;

        }

        public IEnumerable<HomeSlider> GetHomeSliderList()
        {
            return _sliderRepository.GetHomeSliderList();
        }

        public HomeSlider GetHomeSlider(int id)
        {
            return _sliderRepository.GetHomeSlider(id);
        }

        public bool AddOrUpdateHomeSlider(HomeSlider model)
        {
            return _sliderRepository.AddOrUpdateHomeSlider(model);
        }
        public bool DeleteHomeSlider(int id)
        {
            return _sliderRepository.DeleteHomeSlider(id);
        }
        public List<HomeSlider> GetHomeSliders()
        {
            return _sliderRepository.GetHomeSliders();
        }
        //public List<SmartSerach_Result> SmartSearch(string freeText)
        //{
        //    return _sliderRepository.SmartSearch(freeText);
        //}
        public HomeBanner GetHomeBanner()
        {
            return _sliderRepository.GetHomeBanner();
        }
        public bool AddOrUpdateHomeBanner(HomeBanner model)
        {
            return _sliderRepository.AddOrUpdateHomeBanner(model);
        }
        public bool DeleteHomeBanner(Decimal id)
        {
            return _sliderRepository.DeleteHomeBanner(id);
        }
        public IEnumerable<FAQ> GetFAQList()
        {
            return _sliderRepository.GetFAQList();
        }
        public FAQ GetFAQ(Decimal id)
        {
            return _sliderRepository.GetFAQ(id);
        }
        public bool DeleteFAQ(Decimal id)
        {
            return _sliderRepository.DeleteFAQ(id);
        }
        public bool AddOrUpdateFAQ(FAQ model)
        {
            return _sliderRepository.AddOrUpdateFAQ(model);
        }
        public void AddProductToRecentViewed(RecentVisitedProduct model)
        {
            _sliderRepository.AddProductToRecentViewed(model);
        }
        public IEnumerable<ProductEntity> ListproductbyUserId()
        {
            return _sliderRepository.ListproductbyUserId();
        }
        public RecentVisitedProduct GetRecentVisitedProduct(int Id)
        {
            return _sliderRepository.GetRecentVisitedProduct(Id);
        }
    }
}

