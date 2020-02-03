using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University.UI.Models;
using University.UI.Utilities;
using AuthorizeNet.Api.Contracts.V1;
using University.Service.Interface;
using University.Data;
using System.Configuration;

namespace University.UI.Controllers
{
    public class PaymentGatewayController : Controller
    {
        private IPaymentGatewayService _PaymentGatewayService;
        public PaymentGatewayController(IPaymentGatewayService PaymentGatewayService)
        {
            _PaymentGatewayService = PaymentGatewayService;
        }

        // GET: PaymentGateway
        public ActionResult Index()
        {
            return View("PaymentGateway");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PaymentInfo(PaymentGatewayVM PaymentGatewayVM)
        {
            createTransactionResponse response = PaymentGatewayUtility.Run(ConfigurationManager.AppSettings["ApiLoginID"], ConfigurationManager.AppSettings["ApiTransactionKey"], PaymentGatewayVM);
            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                bool TransResult = _PaymentGatewayService.SaveTransactionDetails(new CardTransactionDetails
                {
                    CardHolderName = PaymentGatewayVM.CardHolderName,
                    CardNumber = PaymentGatewayVM.CardNumber,
                    Expiration = PaymentGatewayVM.MonthAndYear,
                    CVV = PaymentGatewayVM.CVV,
                    ResultCode = response.messages.resultCode.ToString(),
                    Message = response.messages.message[0].text,
                    AccountType = response.transactionResponse.accountType,
                    TransId = response.transactionResponse.transId,
                    ResponseCode = Convert.ToInt32(response.transactionResponse.responseCode),
                    CreatedDate = DateTime.Now,
                    //change for user to userloginid
                    CreatedBy = Convert.ToInt32(Session["AdminLoginID"])
                });
                if (TransResult)
                {
                    return Json(new { result = true, Message = response.messages.message[0].text });
                }
                else
                {
                    return Json(new { result = true, Message = response.messages.message[0].text });
                }
            }
            else
            {
                return Json(new { result = false, Message = response.messages.message[0].text });
            }
        }
    }
}
