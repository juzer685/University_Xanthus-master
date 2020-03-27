using University.Data;
using University.Service.Interface;
using University.UI.Areas.Admin.Models;
using University.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPSU.Web.Areas.Admin.Controllers
{
    public class VideoController : BaseController
    {

        private IProductVideoService _productVideoService;
        private IProductService _productService;
        private ISubCategoryService _subCategoryService;

        public VideoController(IProductVideoService productVideoService, IProductService productService,ISubCategoryService subCategoryService) : base(productService)
        {
            _productVideoService = productVideoService;
            _productService = productService;
            _subCategoryService = subCategoryService;
        }
        // GET: Admin/Video
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Video(string SearchString)
        {
            List<ProductVideoViewModel> productVideoViewModel = new List<ProductVideoViewModel>();

            var res = _productVideoService.GetUserVideosList().ToList();

            if (!String.IsNullOrEmpty(SearchString))
            {
                res = res.Where(x => x.Title.ToLower().Contains(SearchString.ToLower())).ToList();
            }
          
            foreach (var pvideo in res)
            {
                productVideoViewModel.Add(new ProductVideoViewModel{
                    Id = pvideo.Id,
                    Title = pvideo.Title,
                    Decription = pvideo.Decription,
                    VideoURL = pvideo.VideoURL,
                    ProductId=pvideo.ProductId,
                    IsPaid=pvideo.IsPaid,
                    VideoRate=pvideo.VideoRate,
                    SubCatID=pvideo.SubcatId
                });
                
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
             productVideoViewModel = productVideoViewModel.Where(x => x.Title.ToLower().Contains(SearchString.ToLower())).ToList();
            }

            return View(productVideoViewModel);
        }
    }
}