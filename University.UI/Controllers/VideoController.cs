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

        public VideoController(IProductVideoService productVideoService, IProductService productService) : base(productService)
        {
            _productVideoService = productVideoService;
            _productService = productService;
        }
        // GET: Admin/Video
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Video(string SearchString)
        {
            List<ProductVideoViewModel> productVideoViewModel = new List<ProductVideoViewModel>();
            var productvideores = _productVideoService.GetProductVideoList().ToList();
            var productList = _productService.GetProductList().ToList();

            var query = from productvideo in productvideores
                        join product in productList
                             on productvideo.ProductId equals product.Id
                        select new ProductVideoViewModel
                        {
                            Title = productvideo.Title,
                            Decription = productvideo.Decription,
                            ProductId = productvideo.ProductId,
                            VideoURL = productvideo.VideoURL
                        };
            if (!String.IsNullOrEmpty(SearchString))
            {
                query = query.Where(x => x.Title.ToLower().Contains(SearchString.ToLower())).ToList();
            }
            return View(query);
        }
    }
}