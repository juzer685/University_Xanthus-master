using University.Data;
using University.Data.CustomEntities;
using University.Service;
using University.Service.Interface;
using University.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace University.UI.Areas.Admin.Controllers
{
    public class HomeSliderController : Controller
    {
        private IHomeService _sliderService;
        private IProductService _productService;
        string HomeSliderImagePath = WebConfigurationManager.AppSettings["HomeSliderImagePath"];

        public HomeSliderController(IHomeService sliderMasterService, IProductService productService)
        {
            _sliderService = sliderMasterService;
            _productService = productService;
        }
        // GET: Admin/Category
        public ActionResult Index()
        {
            var res = _sliderService.GetHomeSliderList().ToList();
            var viewModel = AutoMapper.Mapper.Map<List<HomeSlider>, List<HomeSliderViewModel>>(res);
            return View(viewModel);
        }

        public ActionResult AddEditHomeSlider(HomeSliderViewModel model, HttpPostedFileBase file)
        {
            var res = AutoMapper.Mapper.Map<HomeSliderViewModel, HomeSlider>(model);
            
            if (file != null)
            {
                res.ImageURL = UploadFileOnServer(HomeSliderImagePath, file);
            }
            var isSuccess = _sliderService.AddOrUpdateHomeSlider(res);
            // return Json(isSuccess, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index", isSuccess);
        }
        private string UploadFileOnServer(string location, HttpPostedFileBase file)
        {
            string extension = Path.GetFileName(file.FileName);
            //string fileId = Guid.NewGuid().ToString().Replace("-", "");
            //string filename = fileId + extension;
            var path = Path.Combine(Server.MapPath(location), extension);
            file.SaveAs(path);
            return extension;
        }
        public ActionResult GetHomeSlider(int? Id)
        {
            HomeSliderViewModel model;
            if (Id.HasValue)
            {
                var res = _sliderService.GetHomeSlider(Id.Value);
                model = AutoMapper.Mapper.Map<HomeSlider, HomeSliderViewModel>(res);
            }
            else
            {
                model = new HomeSliderViewModel();
            }

            var productList = _productService.GetProductList();
           // productList = productList.Where(t => t.AssocitedID == Convert.ToInt32(Session["UserSessionIDs"])).ToList();
            var result = AutoMapper.Mapper.Map<IEnumerable<ProductEntity>, List<ProductDropdownListViewModel>>(productList);
            model.Products = result;

            return PartialView("AddEditHomeSlider", model);
        }

        //[HttpDelete]
        public ActionResult DeleteHomeSlider(int Id)
        {
            var res = _sliderService.DeleteHomeSlider(Id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
