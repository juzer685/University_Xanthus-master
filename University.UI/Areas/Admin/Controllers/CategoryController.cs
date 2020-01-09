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
    public class CategoryController : Controller
    {
        private ICategoryMasterService _categoryMasterService;
        string CategoryImagePath = WebConfigurationManager.AppSettings["CategoryImagePath"];

        public CategoryController(ICategoryMasterService categoryMasterService)
        {
            _categoryMasterService = categoryMasterService;
        }
        // GET: Admin/Category
        public ActionResult Index()
        {
            var res = _categoryMasterService.GetCategoryList().ToList();
            var viewModel=AutoMapper.Mapper.Map<List<CategoryMaster>, List<CategoryViewModel>>(res);
            return View(viewModel);
        }

        public ActionResult AddEditCategory(CategoryViewModel model, HttpPostedFileBase file)
        {
            var res= AutoMapper.Mapper.Map<CategoryViewModel, CategoryMaster>(model);
            if (file != null)
            {
                res.ImageURL = UploadFileOnServer(CategoryImagePath, file);
            }
            var isSuccess=_categoryMasterService.AddOrUpdateCategory(res);
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetCategory(string Id)
        {
            CategoryViewModel model;
            if (!string.IsNullOrEmpty(Id))
            {
                var res = _categoryMasterService.GetCategory(Convert.ToDecimal(Id));
                model = AutoMapper.Mapper.Map<CategoryMaster, CategoryViewModel>(res);
            }
            else
            {
                model = new CategoryViewModel();
            }
            
            return PartialView("AddEditCategory",model);
        }

        //[HttpDelete]
        public ActionResult DeleteCategory(string Id)
        {
            var res = _categoryMasterService.DeleteCategory(Convert.ToDecimal(Id));
            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}