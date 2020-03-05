using University.Data;
using University.Data.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repository.Interface
{
    public interface IHomeRepository
    {
        //IEnumerable<CardTransactionDeatilsMapping> GetBuyProductList();
        IEnumerable<HomeSlider> GetHomeSliderList();
        HomeSlider GetHomeSlider(int id);
        bool AddOrUpdateHomeSlider(HomeSlider model);
        bool DeleteHomeSlider(int id);
        List<HomeSlider> GetHomeSliders();
        HomeBanner GetHomeBanner();
        bool AddOrUpdateHomeBanner(HomeBanner model);
        bool DeleteHomeBanner(Decimal id);

        IEnumerable<FAQ> GetFAQList();
        FAQ GetFAQ(Decimal id);
        bool DeleteFAQ(Decimal id);
        bool AddOrUpdateFAQ(FAQ model);
        void AddProductToRecentViewed(RecentVisitedProduct model);
        RecentVisitedProduct GetRecentVisitedProduct(int Id);
        IEnumerable<ProductEntity> ListproductbyUserId();
        //get video list on homepage
        IEnumerable<ProductVideoModel> GetUserVideosList();
    }
}
