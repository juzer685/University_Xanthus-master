using University.Data;
using University.Data.CustomEntities;
using University.Service.Interface;
using University.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University.UI.Controllers;

namespace IPSU.Web.Controllers
{
    public class UserGuideController : BaseController
    {

        // GET: UserGuide

        private IProductUserGuideService _productUserGuideService;

        public UserGuideController(IProductUserGuideService productUserGuideService, IProductService productService) : base(productService)
        {
            _productUserGuideService = productUserGuideService;

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserGuide(string SearchString)
        {
            //var userGuides = _productUserGuideService.GetProductUserGuideList().ToList();
            //var viewModel = AutoMapper.Mapper.Map<List<ProductUserGuide>, List<ProductUserGuideViewModel>>(userGuides);
            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //    viewModel = viewModel.Where(x => x.Title.ToLower().Contains(SearchString.ToLower())).ToList();
            //}
            //return View(viewModel);
            List<ProductUserGuideViewModel> productUserGuideViewModels = new List<ProductUserGuideViewModel>();

            var res = _productUserGuideService.GetUserGuideList().ToList();
            if (!String.IsNullOrEmpty(SearchString))
            {
                res = res.Where(x => x.Title.ToLower().Contains(SearchString.ToLower())).ToList();
            }

            foreach (var pvideo in res)
            {
                productUserGuideViewModels.Add(new ProductUserGuideViewModel
                {
                    Id = pvideo.Id,
                    Title = pvideo.Title,
                    Description = pvideo.Description,
                    ImageURL = pvideo.ImageURL
                });

            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                productUserGuideViewModels = productUserGuideViewModels.Where(x => x.Title.ToLower().Contains(SearchString.ToLower())).ToList();
            }

            return View(productUserGuideViewModels);
        }
    }

}