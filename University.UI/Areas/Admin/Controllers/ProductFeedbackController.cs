using University.Data;
using University.Service.Interface;
using University.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University.UI.Areas.Admin.Controllers
{
    public class ProductFeedbackController : Controller
    {
        private IProductService _productService;

        public ProductFeedbackController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Admin/ProductFeedback
        public ActionResult Index()
        {
            var model=_productService.GetProductFeedback().OrderByDescending(y=>y.CreatedDate).ToList();
            var viewModel = AutoMapper.Mapper.Map<List<ProductFeedback>, List<ProductFeedbackViewModel>>(model);
            return View(viewModel);
        }
    }
}