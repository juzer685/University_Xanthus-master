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
    public class FAQController : Controller
    {
        string FAQImagePath = WebConfigurationManager.AppSettings["FAQImagePath"];
        private IHomeService _homeService;

        public FAQController(IHomeService homeService)
        {
            _homeService = homeService;
        }
        // GET: Admin/FAQ
        public ActionResult Index()
        {
            var model = _homeService.GetFAQList().ToList();
            var viewModel = AutoMapper.Mapper.Map<List<FAQ>, List<FAQViewModel>>(model);
            return View(viewModel);
        }
        public ActionResult GetFAQ(string id)
        {
            var viewModel = new FAQViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var model = _homeService.GetFAQ(Convert.ToDecimal(id));
                viewModel.Id = model.Id;
                viewModel.Question = model.Question;
                viewModel.Answer = model.Answer;
                viewModel.AssocitedCustID = model.AssocitedCustID;
                viewModel.IsDeleted = model.IsDeleted;
                //viewModel = AutoMapper.Mapper.Map<FAQ, FAQViewModel>(model);
            }
            return View("_AddEditFAQ", viewModel);
        }
        public ActionResult AddEditFAQ(FAQViewModel model )
        {
            var res = AutoMapper.Mapper.Map<FAQViewModel, FAQ>(model);
           

            //if (file != null)
            //{
            //    res.ImageURL = UploadFileOnServer(FAQImagePath, file);
            //}
            var isSuccess = _homeService.AddOrUpdateFAQ(res);
            //return Json(isSuccess, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index", isSuccess);
        }

        public ActionResult DeleteFAQ(string Id)
        {
            var res = _homeService.DeleteFAQ(Convert.ToDecimal(Id));
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