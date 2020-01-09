using University.Data;
using University.Data.CustomEntities;
using University.Service;
using University.Service.Interface;
using University.UI.Areas.Admin.Models;
using University.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using University.UI.Controllers;
using University.UI.Utilities;

namespace IPSU.Web.Controllers
{
    public class ProductsController : BaseController
    {
        // GET: Product

        string ProductImagePath = WebConfigurationManager.AppSettings["ProductImagePath"];
        private ICategoryMasterService _categoryMasterService;
        private ISubCategoryService _subCategoryService;
        private IProductService _productService;
        private IFeedbackService _feedbackService;
        private IHomeService _homeService;
        public ProductsController(ICategoryMasterService categoryMasterService, IFeedbackService feedbackService,
            ISubCategoryService subCategoryService, IProductService productService, IHomeService homeService) : base(productService)
        {
            _categoryMasterService = categoryMasterService;
            _subCategoryService = subCategoryService;
            _productService = productService;
            _feedbackService = feedbackService;
            _homeService = homeService;
        }
        public ActionResult Category()
        {
            return View();
        }

        public ActionResult ProductListByCategory(Decimal id)
        {
            var res = _productService.GetProductList().Where(x => x.SubCategoryId == id && x.IsDeleted != true).ToList();
            var viewModel = AutoMapper.Mapper.Map<List<ProductEntity>, List<ProductViewModel>>(res);
            if (viewModel.Count != 0)
            {
                ViewBag.CategoryName = res.Where(x => x.SubCategoryMaster.Id == res.First().SubCategoryId).FirstOrDefault().SubCategoryMaster.Name;
                return View(viewModel);

            }
            else
            {
                return RedirectToAction("Category", "Home");
            }


        }

        public ActionResult ProductsById(string ProductId)
        {
            ProductViewModel viewModel;
            if (!string.IsNullOrEmpty(ProductId))
            {
                var res = _productService.GetProduct(Convert.ToDecimal(UrlSecurityManager.Decrypt(ProductId, System.Configuration.ConfigurationManager.AppSettings["SecurityKey"])));
                if (res != null)
                {
                    viewModel = AutoMapper.Mapper.Map<ProductEntity, ProductViewModel>(res);
                    if (viewModel.ProductUserGuide == null)
                    {
                        viewModel.ProductUserGuide = new ProductUserGuideViewModel();
                    }
                    if (viewModel.ProductFAQs != null)
                    {
                        foreach (var item in viewModel.ProductFAQs)
                        {
                            var productFaq = _productService.GetProductFAQ(item.Id);
                            var viewModelFAQ = AutoMapper.Mapper.Map<ProductFAQs, ProductFaqViewModel>(productFaq);
                            if (viewModelFAQ.ProductFAQVideoList == null) { viewModelFAQ.ProductFAQVideoList = new List<ProductFAQVideoViewModel>(); }
                            else
                            {
                                var videos = AutoMapper.Mapper.Map<List<ProductFAQVideos>, List<ProductFAQVideoViewModel>>(productFaq.ProductFAQVideos.ToList());
                                viewModelFAQ.ProductFAQVideoList = videos.Where(y => y.IsDeleted != true).ToList();
                            }
                            item.ProductFAQVideoList = viewModelFAQ.ProductFAQVideoList;
                        }
                    }
                }
                else
                {
                    viewModel = new ProductViewModel();
                    return Redirect("/Home/Index");
                }
            }
            else
            {
                viewModel = new ProductViewModel();
                return Redirect("/Home/Index");
            }

            ViewBag.SubCategoryList = _subCategoryService.GetSubCategoryList();
            //_homeService.AddProductToRecentViewed(new RecentVisitedProduct() { ProductId = ProductId, UserId = Guid.Parse("6A68CDB1-167F-4323-9D90-34602D20E06D") });
            _homeService.AddProductToRecentViewed(new RecentVisitedProduct() { ProductId = Convert.ToDecimal(UrlSecurityManager.Decrypt(ProductId, "iPsWeBApi2018")), UserId = Convert.ToInt32(Session["UserId"]) });

            return View(viewModel);
        }

        public ActionResult ProductFeedback(string message, string id)
        {
            ProductFeedback productFeedback = new ProductFeedback();
            productFeedback.FeedbackDescription = message;
            productFeedback.IsDeleted = false;
            productFeedback.ProductId = Convert.ToDecimal(id);
            productFeedback.CreatedDate = DateTime.Now;
            bool savefeedback = _feedbackService.SaveFeedback(productFeedback);
            return this.Json("Thank you for your feedback.", JsonRequestBehavior.AllowGet);

        }
    }
}