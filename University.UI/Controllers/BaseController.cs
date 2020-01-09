using University.Service;
using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University.UI.Controllers
{
    public class BaseController : Controller
    {
        private IProductService _productService;
        private ILoginService loginPageService;

        public BaseController(IProductService productService)
        {
            _productService = productService;
            SetProductsForMenu();
        }

        public BaseController(ILoginService loginPageService)
        {
            this.loginPageService = loginPageService;
        }

        private void SetProductsForMenu()
        {
            var menu= _productService.GetProductLayouMenu();
            System.Web.HttpContext.Current.Session.Add("_LayoutProductMenu", menu);
        }
    }
}