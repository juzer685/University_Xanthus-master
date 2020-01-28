using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University.UI.Utilities;

namespace University.UI.Controllers
{
    public class PaymentGatewayController : Controller
    {
        // GET: PaymentGateway
        public ActionResult Index()
        {
            PaymentGatewayUtility.Run("8JJhA42nA", "8u5wP48YKh8BA8JY");
            return Content("done");
        }
    }
}