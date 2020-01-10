using University.Data;
using University.Service;
using University.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using University.Service.Interface;

namespace University.UI.Areas.Admin.Controllers
{
    public class HomeBannerController : Controller
    {
        string HomeBannerImagePath = WebConfigurationManager.AppSettings["HomeBannerImagePath"];
        private IHomeService _homeService;

        public HomeBannerController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public ActionResult Index()
        {
            
            var model = _homeService.GetHomeBanner();
            var viewModel = AutoMapper.Mapper.Map<HomeBanner, HomeBannerViewModel>(model);
            if (viewModel == null)
            {
                viewModel = new HomeBannerViewModel();
            }
            return View(viewModel);
        }
        public ActionResult AddEditHomeBanner(HomeBannerViewModel ViewModel, HttpPostedFileBase file)
        {
            var model = AutoMapper.Mapper.Map<HomeBannerViewModel, HomeBanner>(ViewModel);
            if (file != null)
            {
                model.ImageURL = UploadFileOnServer(HomeBannerImagePath, file);
            }
            var res = _homeService.AddOrUpdateHomeBanner(model);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteHomeBanner(string Id)
        {
            var res = _homeService.DeleteHomeBanner(Convert.ToDecimal(Id));
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        private string UploadFileOnServer(string location, HttpPostedFileBase file)
        {
            string extension = Path.GetExtension(file.FileName);
            string fileId = Guid.NewGuid().ToString().Replace("-", "");
            string filename = fileId + extension;
            var path = Path.Combine(Server.MapPath(location), filename);
            file.SaveAs(path);
            return filename;
        }

       
    }
}