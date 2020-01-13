using University.Data;
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
    public class SubCategoryController : Controller
    {
        string SubCategoryImagePath = WebConfigurationManager.AppSettings["SubCategoryImagePath"];
        private ICategoryMasterService _categoryMasterService;
        private ISubCategoryService _subCategoryService;
       
        public SubCategoryController(ICategoryMasterService categoryMasterService, ISubCategoryService subCategoryService)
        {
            _categoryMasterService = categoryMasterService;
            _subCategoryService = subCategoryService;
            
        }

        // GET: Admin/SubCategory
        public ActionResult Index()
        {
            var res = _subCategoryService.GetSubCategoryList().ToList();
            var viewModel = AutoMapper.Mapper.Map<List<SubCategoryMaster>, List<SubCategoryViewModel>>(res);
            //ViewBag.CategoryList = _categoryMasterService.GetCategoryList();
            
            return View(viewModel);
        }

        public ActionResult GetSubCategory(string Id)
        {
            SubCategoryViewModel model;
            if (!string.IsNullOrEmpty(Id))
            {
                var res = _subCategoryService.GetSubCategory(Convert.ToDecimal(Id));
                model = AutoMapper.Mapper.Map<SubCategoryMaster, SubCategoryViewModel>(res);
            }
            else
            {
                model = new SubCategoryViewModel();
            }
            ViewBag.CategoryList = _categoryMasterService.GetCategoryList();
            return PartialView("AddEditSubCategory", model);
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
        public ActionResult AddEditSubCategory(SubCategoryViewModel model, HttpPostedFileBase file)
        {
            var res = AutoMapper.Mapper.Map<SubCategoryViewModel, SubCategoryMaster>(model);
            if (file != null)
            {
                res.ImageURL = UploadFileOnServer(SubCategoryImagePath, file);
            }
            var isSuccess = _subCategoryService.AddOrUpdateSubCategory(res);
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteSubCategory(string Id)
        {
            var res = _subCategoryService.DeleteSubCategory(Convert.ToDecimal(Id));
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}