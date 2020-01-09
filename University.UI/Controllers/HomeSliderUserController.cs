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
using University.Service;

namespace IPSU.Web.Controllers
{
    public class HomeSliderUserController :Controller
    {
        private IUserHomeSliderService _userHomeSliderService;
        public HomeSliderUserController() 
        {
            _userHomeSliderService = new UserHomeSliderService();

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SliderUser()
        {
            var userGuides = _userHomeSliderService.GetProductUserGuideList().ToList();
            var viewModel = AutoMapper.Mapper.Map<List<HomeSlider>, List<HomeSlider>>(userGuides);
            return View(viewModel);
        }
        //public ActionResult UserGuide(string SearchString)
        //{
        //    var userGuides = _productUserGuideService.GetProductUserGuideList().ToList();
        //    var viewModel = AutoMapper.Mapper.Map<List<ProductUserGuide>, List<ProductUserGuideViewModel>>(userGuides);
        //    if (!String.IsNullOrEmpty(SearchString))
        //    {
        //        viewModel = viewModel.Where(x => x.Title.ToLower().Contains(SearchString.ToLower())).ToList();
        //    }
        //    return View(viewModel);
        //}
    }

}