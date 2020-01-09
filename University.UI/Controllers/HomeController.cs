using University.Data;
using University.Data.CustomEntities;
using University.Service;
using University.Service.Interface;
using University.UI.Areas.Admin.Models;
using University.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace University.UI.Controllers
{
    public class HomeController : BaseController
    {
        private ICategoryMasterService _categoryMasterService;
        private IFeedbackService _feedbackService;
        private ISubCategoryService _subCategoryService;
        private IProductService _productService;
        private IHomeService _sliderService;
        private IUseradminService _UseradminService;
        public HomeController(ICategoryMasterService categoryMasterService, IFeedbackService feedbackService,
            ISubCategoryService subCategoryService, IProductService productService, IHomeService sliderMasterService) : base(productService)
        {
            _categoryMasterService = categoryMasterService;
            _feedbackService = feedbackService;
            _subCategoryService = subCategoryService;
            _productService = productService;
            _sliderService = sliderMasterService;
            _UseradminService = new UseradminService();

        }

        //[SsoAuth]
        public ActionResult Index()
        {
            SetIdentityDetails();

            var res = _sliderService.GetHomeSliderList().ToList();
            var viewModel = AutoMapper.Mapper.Map<List<HomeSlider>, List<HomeSliderViewModel>>(res);

            var Bannermodel = _sliderService.GetHomeBanner();
            var BannerviewModel = AutoMapper.Mapper.Map<HomeBanner, HomeBannerViewModel>(Bannermodel);


            List<ProductFAQs> ListProductFAQ = new List<ProductFAQs>();
            ProductFAQs PF = new ProductFAQs();
            PF.Question = "How does credit card acceptance work?";
            PF.Answer = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
            ListProductFAQ.Add(PF);
            ListProductFAQ.Add(PF);
            ListProductFAQ.Add(PF);
            ListProductFAQ.Add(PF);
            ListProductFAQ.Add(PF);
            List<ProductEntity> ListProduct = new List<ProductEntity>();
            ListProduct = _sliderService.ListproductbyUserId().ToList();
            RecentVisitedProduct recentVisitedProduct = new RecentVisitedProduct();
            // recentVisitedProduct =null
            recentVisitedProduct = _sliderService.GetRecentVisitedProduct(Convert.ToInt32(Session["UserId"]));


            var model = _sliderService.GetFAQList().ToList();
            var FAQviewModel = AutoMapper.Mapper.Map<List<FAQ>, List<FAQViewModel>>(model);

            HomePageViewModel homePageViewModel = new HomePageViewModel();
            if (recentVisitedProduct != null)
            {
                homePageViewModel.LastViewedProduct = recentVisitedProduct.Product.Title.ToString();
            }
            else
            {
                homePageViewModel.LastViewedProduct = "None";
            }
            homePageViewModel.HomeBanners = viewModel.ToList();
            homePageViewModel.ProductFAQs = FAQviewModel;
            homePageViewModel.Products = ListProduct;
            HomeBanner homeBanner = new HomeBanner();
            if (BannerviewModel != null)
            {
                homeBanner.ImageURL = BannerviewModel.ImageFullPath;
                homeBanner.LinkTo = BannerviewModel.LinkTo;


            }


            homePageViewModel.BannerImage = homeBanner;
            return View(homePageViewModel);
        }


        private void SetIdentityDetails()
        {
            int webUserId = 95665;
            if (Session["Customer"] == null)
            {
                Session["Customer"] = 1;//SSOUser.CustomerID;
            }
            Session["CustomerName"] = "John Doe";//SSOUser.CustomerName;
            Session["UserName"] = "System Admin";//SSOUser.UserName;
            Session["UserId"] = 95665;//SSOUser.WebUserID;
            Session["IPSUserID"] = 95665; //SSOUser.WebUserID;
            Session["Role"] = 1;//SSOUser.RoleID;
            Session["UserType"] = "IPSUser";
            Session["CustomerList"] = null;//customerList;
            Session["CustomerList"] = null;//customerList;
            int Customer = Convert.ToInt32(Session["Customer"]);
            if (Session["CurrentCustomerID"] == null)
            {
                Session["CurrentCustomerID"] = Customer;
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Feedback(string message)
        {
            GeneralFeedback generalFeedback = new GeneralFeedback();
            generalFeedback.FeedbackDescription = message;
            generalFeedback.IsDeleted = false;
            generalFeedback.CreatedDate = DateTime.Now;
            bool savefeedback = _feedbackService.SaveGeneralFeedback(generalFeedback);
            return this.Json("Thank you for your feedback.", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Category()
        {
            HomePageViewModel homePageViewModel = new HomePageViewModel();
            homePageViewModel.SubCategories = _subCategoryService.GetSubCategoryListOnlyHaveProduct().ToList();
            return View(homePageViewModel);
        }
        public ActionResult SubCategory(string id)
        {
            //Guid id = Guid.Parse("907F10C8-2B22-443A-A166-367D0E5F31DD");
            HomePageViewModel homePageViewModel = new HomePageViewModel();
            homePageViewModel.SubCategories = _subCategoryService.GetSubCategoryListById(Convert.ToDecimal(id));
            return View(homePageViewModel);
            // return RedirectToAction("SubCategory","Home",new {data= homePageViewModel});
        }
        //public ActionResult SubCategory(HomePageViewModel homePageViewModel)
        //{
        //    return View(homePageViewModel);
        //}
        public ActionResult Search(string SearchString)
        {
            List<ProductEntity> listproductEntity = new List<ProductEntity>();
            listproductEntity = _productService.GetProducts().Where(x => x.ProductUserGuide != null && x.ProductVideos != null).ToList();
            listproductEntity = listproductEntity.Where(x => x.ProductUserGuide.Title.ToLower().Contains(SearchString.ToLower()) || x.ProductSpec.Description.ToLower().Contains(SearchString.ToLower()) || x.Description.ToLower().Contains(SearchString.ToLower()) || x.Title.ToLower().Contains(SearchString.ToLower())).ToList();
            ViewBag.SearchString = SearchString;
            return View(listproductEntity);
        }
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult SmartSearch(string freeText)
        {
            ViewBag.FreeText = freeText;
            var res = _categoryMasterService.SmartSearch(freeText);
            return View(res);
        }

        [HttpPost]
        public JsonResult UserSessionID (int ID)
        {
            Session["UserSessionIDs"] = ID;
            //return RedirectToAction("");
            return Json(new { success=true},JsonRequestBehavior.AllowGet);
        }
           
      




    }
}