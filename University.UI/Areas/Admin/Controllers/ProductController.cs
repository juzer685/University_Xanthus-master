using University.Data;
using University.Data.CustomEntities;
using University.Service;
using University.Service.Interface;
using University.UI.Areas.Admin.Models;
using University.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace University.UI.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        string ProductImagePath = WebConfigurationManager.AppSettings["ProductImagePath"];
        private ICategoryMasterService _categoryMasterService;
        private ISubCategoryService _subCategoryService;
        private IProductService _productService;
        

        public ProductController(ICategoryMasterService categoryMasterService, ISubCategoryService subCategoryService, IProductService productService)
        {
            _categoryMasterService = categoryMasterService;
            _subCategoryService = subCategoryService;
            _productService = productService;
        }
        // GET: Admin/Product
        public ActionResult Index()
        {
            var res = _productService.GetProductList().ToList();
            var viewModel = AutoMapper.Mapper.Map<List<ProductEntity>, List<ProductViewModel>>(res);
            return View(viewModel);
        }
        public ActionResult AddEditProduct(string ProductId)
        {
            ProductViewModel viewModel;
            if (!string.IsNullOrEmpty(ProductId))
            {
                var res = _productService.GetProduct(Convert.ToDecimal(ProductId));
                viewModel = AutoMapper.Mapper.Map<ProductEntity, ProductViewModel>(res);
                if (viewModel.ProductUserGuide == null) { viewModel.ProductUserGuide = new ProductUserGuideViewModel(); }
            }
            else
            {
                viewModel = new ProductViewModel();
            }
            var subcategorys = _subCategoryService.GetSubCategoryList().ToList();
            subcategorys = subcategorys.Where(t => t.AssocitedID == Convert.ToInt32(Session["UserSessionIDs"])).ToList();
            ViewBag.SubCategoryList = subcategorys;
            return View(viewModel);
        }
        public ActionResult LoadProductBasicDetails(string ProductId)
        {
            ProductViewModel viewModel;
            if (!string.IsNullOrEmpty(ProductId) && !ProductId.Equals("0"))
            {
                var res = _productService.GetProduct(Convert.ToDecimal(ProductId));
                viewModel = AutoMapper.Mapper.Map<ProductEntity, ProductViewModel>(res);
                if (viewModel.ProductUserGuide == null) { viewModel.ProductUserGuide = new ProductUserGuideViewModel(); }
            }
            else
            {
                viewModel = new ProductViewModel();
            }
            ViewBag.SubCategoryList = _subCategoryService.GetSubCategoryList();
            return View("_AddEditProductBasicDetails", viewModel);
        }
        //public ActionResult SaveProduct(ProductViewModel model, HttpPostedFileBase file, HttpPostedFileBase Guidefile,HttpPostedFileBase Videofile, HttpPostedFileBase[] FaqVideo)
        //{
        //    var res = AutoMapper.Mapper.Map<ProductViewModel, ProductEntity>(model);
        //    if (file != null)
        //    {
        //        res.ImageURL = UploadFileOnServer(ProductImagePath, file);
        //    }
        //    if (Guidefile != null)
        //    {
        //        res.ProductUserGuide.ImageURL = UploadFileOnServer(ProductImagePath, Guidefile);
        //    }
        //    if (Videofile != null)
        //    {
        //        res.ProductVideo.VideoURL = UploadFileOnServer(ProductImagePath, Videofile);
        //    }
        //    var isSuccess = _productService.AddOrUpdateProduct(res);
        //    return Json(isSuccess, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult SaveProduct(ProductViewModel model, HttpPostedFileBase file)
        {
            if (model.Id == 0)
            {
                var res = AutoMapper.Mapper.Map<ProductViewModel, ProductEntity>(model);
                if (file != null)
                {
                    res.ImageURL = UploadFileOnServer(ProductImagePath, file);
                }
                var productId = _productService.AddUpdateProductBasic(res);
                return Json(productId, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var res = AutoMapper.Mapper.Map<ProductViewModel, ProductEntity>(model);
                if (file != null)
                {
                    res.ImageURL = UploadFileOnServer(ProductImagePath, file);
                }
                var productId = _productService.AddUpdateProductBasic(res);
                return Json(productId, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetProductFAQ(string ProductId)
        {
            var product = _productService.GetProduct(Convert.ToDecimal(ProductId));
            var viewModel = AutoMapper.Mapper.Map<ProductEntity, ProductViewModel>(product ?? new ProductEntity());
            if (viewModel.ProductFAQs.Count == 0) { viewModel.ProductFAQs = new List<ProductFaqViewModel>(); }
            return View("_ProductFAQ", viewModel);
        }
        public ActionResult SaveProductProductFAQ(ProductFaqViewModel model, string FaqId)
        {
            if (FaqId.ToString() == "0")
            {
                model.AssocitedID = Convert.ToInt32(Session["UserSessionIDs"]);
                model.Id = Convert.ToDecimal(FaqId);
                var res = AutoMapper.Mapper.Map<ProductFaqViewModel, ProductFAQs>(model);
                var productFaqId = _productService.SaveProductFAQ(res);
                var productFaq = _productService.GetProductFAQ(productFaqId);
                var viewModel = AutoMapper.Mapper.Map<ProductFAQs, ProductFaqViewModel>(productFaq);
                if (viewModel == null) { viewModel = new ProductFaqViewModel(); }
                return View("_ProductFAQForm", viewModel);
            }
            else
            {
                model.AssocitedID = Convert.ToInt32(Session["UserSessionIDs"]);
                model.Id = Convert.ToDecimal(FaqId);
                var res = AutoMapper.Mapper.Map<ProductFaqViewModel, ProductFAQs>(model);
                var productFaqId = _productService.SaveProductFAQ(res);
                var productFaq = _productService.GetProductFAQ(productFaqId);
                var viewModel = AutoMapper.Mapper.Map<ProductFAQs, ProductFaqViewModel>(productFaq);
                if (viewModel == null) { viewModel = new ProductFaqViewModel(); }
                return View("_ProductFAQForm", viewModel);
            }
        }
        public ActionResult DeleteProductFAQ(string FaqId)
        {
            var res = _productService.DeleteProductFAQ(Convert.ToDecimal(FaqId));
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEditFAQVideos(string FaqId)
        {
            var productFaq = _productService.GetProductFAQ(Convert.ToDecimal(FaqId));
            ProductFaqViewModel viewModel = AutoMapper.Mapper.Map<ProductFAQs, ProductFaqViewModel>(productFaq);
            if (viewModel != null)
            {
                var videos = AutoMapper.Mapper.Map<List<ProductFAQVideos>, List<ProductFAQVideoViewModel>>(productFaq.ProductFAQVideos.ToList());
                videos.ForEach(x => x.VideoURL = x.VideoURL.Trim());
                viewModel.ProductFAQVideoList = videos.Where(y => y.IsDeleted != true).ToList();
            }
           else
            {
                viewModel = new ProductFaqViewModel();
            }
            return View("_FAQVideos", viewModel);
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
        public ActionResult GetSubCategoryList(string CategoryId)
        {
            var res = _subCategoryService.GetSubCategoryList(Convert.ToDecimal(CategoryId)).ToList();
            var viewModel = AutoMapper.Mapper.Map<List<SubCategoryMaster>, List<DropDownViewModel>>(res);
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteProduct(string Id)
        {
            var res = _productService.DeleteProduct(Convert.ToDecimal(Id));
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveProductProductFAQVideo(ProductFAQVideoViewModel model, int FaqVideoId)
        {
            model.Id = FaqVideoId;
            var res = AutoMapper.Mapper.Map<ProductFAQVideoViewModel, ProductFAQVideos>(model);
            if (model.FAQVideo != null)
            {
                res.VideoURL = UploadFileOnServer(ProductImagePath, model.FAQVideo);
            }
            if (model.FAQVideoImg != null)
            {
                res.ImageURL = UploadFileOnServer(ProductImagePath, model.FAQVideoImg);
            }
            var productFaqVideoId = _productService.SaveProductFAQVideo(res);
            var productFaqVideo = _productService.GetProductFAQVideo(productFaqVideoId);
            var viewModel = AutoMapper.Mapper.Map<ProductFAQVideos, ProductFAQVideoViewModel>(productFaqVideo);
            if (viewModel == null) { viewModel = new ProductFAQVideoViewModel(); }
            return View("_FAQVideoForm", viewModel);
        }
        public ActionResult DeleteProductFAQVideo(int FaqVideoId)
        {
            var res = _productService.DeleteProductFAQVideo(FaqVideoId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlayVideo(string url)
        {
            return View("_PlayVideo", new VideoViewModel() { Url = url });
        }

        #region Specification
        public ActionResult LoadProductSpecifiction(string ProductId)
        {
            var res = _productService.GetProductSpecification(Convert.ToDecimal(ProductId));
            var viewModel = AutoMapper.Mapper.Map<ProductSpec, ProductSpecViewModel>(res);
            if (viewModel == null) { viewModel = new ProductSpecViewModel(); }
            return View("_ProductSpec", viewModel);
        }

        public ActionResult SaveProductSpecifiction(ProductSpecViewModel model, string SpecId)
        {
            if (SpecId.ToString() == "0")
            {
                model.Id = Convert.ToDecimal(SpecId);
                var viewModel = AutoMapper.Mapper.Map<ProductSpecViewModel, ProductSpec>(model);
                var res = _productService.SaveProductSpecification(viewModel);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {

                model.Id = Convert.ToDecimal(SpecId);
                var viewModel = AutoMapper.Mapper.Map<ProductSpecViewModel, ProductSpec>(model);
                var res = _productService.SaveProductSpecification(viewModel);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Specification

        #region User Guide
        public ActionResult LoadProductUserGuide(string ProductId)
        {
            var res = _productService.GetProductUserGuide(Convert.ToDecimal(ProductId));
            var viewModel = AutoMapper.Mapper.Map<ProductUserGuide, ProductUserGuideViewModel>(res);
            if (viewModel == null) { viewModel = new ProductUserGuideViewModel(); }
            return View("_ProductUserGuide", viewModel);
        }

        public ActionResult SaveProductUserGuide(ProductUserGuideViewModel model, string UserGuideId)
        {
            if (UserGuideId.ToString() == "0")
            {
                model.AssocitedID = Convert.ToInt32(Session["UserSessionIDs"]);
                model.Id = Convert.ToDecimal(UserGuideId);
                if (model.Guidefile != null)
                {
                    model.ImageURL = UploadFileOnServer(ProductImagePath, model.Guidefile);
                }
                //else if (IsDeletedImg)
                //{
                //    model.ImageURL = "";
                //}
                var viewModel = AutoMapper.Mapper.Map<ProductUserGuideViewModel, ProductUserGuide>(model);
                var res = _productService.SaveProductUserGuide(viewModel);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.AssocitedID = Convert.ToInt32(Session["UserSessionIDs"]);
                model.Id = Convert.ToDecimal(UserGuideId);
                if (model.Guidefile != null)
                {
                    model.ImageURL = UploadFileOnServer(ProductImagePath, model.Guidefile);
                }
                //else if (IsDeletedImg)
                //{
                //    model.ImageURL = "";
                //}
                var viewModel = AutoMapper.Mapper.Map<ProductUserGuideViewModel, ProductUserGuide>(model);
                var res = _productService.SaveProductUserGuide(viewModel);
                return Json(true, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion Specification

        #region Product Video
        public ActionResult GetProductVideos(string ProductId)
        {
            var product = _productService.GetProduct(Convert.ToDecimal(ProductId));
            var viewModel = AutoMapper.Mapper.Map<ProductEntity, ProductViewModel>(product ?? new ProductEntity());
            if (viewModel.ProductVideos.Count == 0) { viewModel.ProductVideos = new List<ProductVideoViewModel>(); }
            return View("_ProductVideos", viewModel);
        }
        public ActionResult SaveProductVideo(ProductVideoViewModel model, string ProductVideoId)
        {
            
            if (ProductVideoId.ToString() == "0")
            {
                model.AssocitedID = Convert.ToInt32(Session["UserSessionIDs"]);
                model.Id = Convert.ToDecimal(ProductVideoId);
                var res = AutoMapper.Mapper.Map<ProductVideoViewModel, ProductVideos>(model);
                if (model.ProductVideo != null)
                {
                    res.VideoURL = UploadFileOnServer(ProductImagePath, model.ProductVideo);
                }
                if (model.ProductVideoImg != null)
                {
                    res.ThumbnailURL = UploadFileOnServer(ProductImagePath, model.ProductVideoImg);
                }
                //model.isproductvideofiled = 1;
                var productVideoId = _productService.SaveProductVideo(res);
                var productVideo = _productService.GetProductVideo(productVideoId);
                var viewModel = AutoMapper.Mapper.Map<ProductVideos, ProductVideoViewModel>(productVideo);
                if (viewModel == null) { viewModel = new ProductVideoViewModel(); }
                return View("_ProductVideoForm", viewModel);
            }
            else
            {
                model.AssocitedID = Convert.ToInt32(Session["UserSessionIDs"]);
                model.Id = Convert.ToDecimal(ProductVideoId);
                var res = AutoMapper.Mapper.Map<ProductVideoViewModel, ProductVideos>(model);
                if (model.ProductVideo != null)
                {
                    res.VideoURL = UploadFileOnServer(ProductImagePath, model.ProductVideo);
                }
                if (model.ProductVideoImg != null)
                {
                    res.ThumbnailURL = UploadFileOnServer(ProductImagePath, model.ProductVideoImg);
                }
          
                var productVideoId = _productService.SaveProductVideo(res);
                var productVideo = _productService.GetProductVideo(productVideoId);
                var viewModel = AutoMapper.Mapper.Map<ProductVideos, ProductVideoViewModel>(productVideo);
                if (viewModel == null) { viewModel = new ProductVideoViewModel(); }
                return View("_ProductVideoForm", viewModel);
                
            }
        }


        public ActionResult DeleteProductVideo(string ProductVideoId)
        {
            var res = _productService.DeleteProductVideo(Convert.ToDecimal(ProductVideoId));
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region Product Document
        public ActionResult GetProductDocuments(string ProductId)
        {
            var product = _productService.GetProduct(Convert.ToDecimal(ProductId));
            var viewModel = AutoMapper.Mapper.Map<ProductEntity, ProductViewModel>(product ?? new ProductEntity());
            if (viewModel.ProductDocuments.Count == 0) { viewModel.ProductDocuments = new List<ProductDocumentViewModel>(); }
            return View("_ProductDocument", viewModel);
        }
        public ActionResult SaveProductDocument(ProductDocumentViewModel model, Decimal ProductDocumentId)
        {
            if (ProductDocumentId.ToString() == "0")
            {
                model.AssocitedID = Convert.ToInt32(Session["UserSessionIDs"]);
                model.Id = Convert.ToDecimal(ProductDocumentId);
                var res = AutoMapper.Mapper.Map<ProductDocumentViewModel, ProductDocuments>(model);
                if (model.ProductDocumentFile != null)
                {
                    res.DocumentURL = UploadFileOnServer(ProductImagePath, model.ProductDocumentFile);
                }

                var productDoctId = _productService.SaveProductDocument(res);
                var productDoc = _productService.GetProductDocument(productDoctId);
                var viewModel = AutoMapper.Mapper.Map<ProductDocuments, ProductDocumentViewModel>(productDoc);
                if (viewModel == null) { viewModel = new ProductDocumentViewModel(); }
                return View("_ProductDocumentForm", viewModel);
            }
            else
            {
                model.AssocitedID = Convert.ToInt32(Session["UserSessionIDs"]);
                model.Id = Convert.ToDecimal(ProductDocumentId);
                var res = AutoMapper.Mapper.Map<ProductDocumentViewModel, ProductDocuments>(model);
                if (model.ProductDocumentFile != null)
                {
                    res.DocumentURL = UploadFileOnServer(ProductImagePath, model.ProductDocumentFile);
                }

                var productDoctId = _productService.SaveProductDocument(res);
                var productDoc = _productService.GetProductDocument(productDoctId);
                var viewModel = AutoMapper.Mapper.Map<ProductDocuments, ProductDocumentViewModel>(productDoc);
                if (viewModel == null) { viewModel = new ProductDocumentViewModel(); }
                return View("_ProductDocumentForm", viewModel);
            }
        }
        public ActionResult DeleteProductDocument(string ProductDocumentId)
        {
            var res = _productService.DeleteProductDocument(Convert.ToDecimal(ProductDocumentId));
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}