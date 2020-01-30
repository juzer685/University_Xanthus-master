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

        public ActionResult PaymentInfo(PaymentGatewayVM PaymentGatewayVM)
        {
            createTransactionResponse response = PaymentGatewayUtility.Run("8JJhA42nA", "8u5wP48YKh8BA8JY", PaymentGatewayVM);
            _PaymentGatewayService.SaveTransactionDetails(new CardTransactionDetail
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
            return Json(new { result = true, Message = response.messages.message[0].text });
        }
    }
}
