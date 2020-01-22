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
        private IUseradminService _UseradminService;

        public SubCategoryController(ICategoryMasterService categoryMasterService, ISubCategoryService subCategoryService)
        {
            _categoryMasterService = categoryMasterService;
            _subCategoryService = subCategoryService;
            _UseradminService = new UseradminService();

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

        public ActionResult CategoryUserMappingList()
        {
            var res = _subCategoryService.GetCategoryUserMappingList();
            var res1 = _subCategoryService.GetCategoryUserMappingGrid();
            var viewModel = new CategoryMappingModel
            {
                Logintbllst = res.Item1,
                SubCategoryMasterlst = res.Item2,
                CategoryUserMapping = res1
            };
            //var viewModel = AutoMapper.Mapper.Map<List<CategoryUserMapping>, List<CategoryMappingModel>>(res);
            // var viewModel = AutoMapper.Mapper.Map<CategoryUserMapping, CategoryMappingModel>(res);
            return View(viewModel);
        }
        public ActionResult GetCategoryUserMapping(string Id)
        {
            CategoryMappingModel model;
            // var res="";
             
            if (!string.IsNullOrEmpty(Id))
            {
                var  res = _subCategoryService.GetCategoryUserMapping(Convert.ToDecimal(Id));
                model = AutoMapper.Mapper.Map<CategoryUserMapping, CategoryMappingModel>(res);
            }
            else
            {
                model = new CategoryMappingModel();
            }
            //ViewBag.CategoryList = _categoryMasterService.GetCategoryList();
            return RedirectToAction("CategoryUserMappingList", model);
        }
        public ActionResult AddCategoryUserMapping(CategoryUserMapping model)
        {
            // var res = AutoMapper.Mapper.Map<CategoryMappingModel, CategoryUserMapping>(model);
            var isSuccess = _subCategoryService.AddCategoryUserMapping(model);
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("CategoryUserMapping");
        }

    }
}