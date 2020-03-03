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
using University.Data.CustomEntities;
using University.UI.Areas.Admin.Models;

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

        //change admin email and ID to user when migrating to user
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PaymentInfo(PaymentGatewayVM PaymentGatewayVM)
        {
            createTransactionResponse response = PaymentGatewayUtility.MakePayment(ConfigurationManager.AppSettings["ApiLoginID"], ConfigurationManager.AppSettings["ApiTransactionKey"], PaymentGatewayVM);
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
                    CreatedBy = Convert.ToInt32(Session["UserLoginID"]),
                    Amount=PaymentGatewayVM.Amount,
                    ProductName=PaymentGatewayVM.ProductName,
                    CustomerFullName=PaymentGatewayVM.CustomerFName
                });
                if (TransResult)
                {
                    TempData["PaymentStatus"] = true;
                    return Json(new { result = true, Message = response.messages.message[0].text });
                }
                else
                {
                    TempData["PaymentStatus"] = true;
                    return Json(new { result = true, Message = response.messages.message[0].text });
                }
            }
            else
            {
                TempData["PaymentStatus"] = false;
                return Json(new { result = false, Message = response.messages.message[0].text });
            }
        }

        [HttpGet]
        public ActionResult PaymentStatus()
        {
            return View("PaymentStatus");
        }

        //[HttpGet]
        public ActionResult SaveCardDetails()
        {
            return View("CardDetails", new PaymentGatewayVM());
        }

        //change admin email and ID to user when migrating to user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCardDetails(PaymentGatewayVM PaymentGatewayVM)
        {
            //getCustomerProfileResponse response = PaymentGatewayUtility.GetCustomerProfile(ConfigurationManager.AppSettings["ApiLoginID"], ConfigurationManager.AppSettings["ApiTransactionKey"], /*Session["UserEmail"].ToString(),*/ PaymentGatewayVM);

            var response = PaymentGatewayUtility.CreateCustomerProfile(ConfigurationManager.AppSettings["ApiLoginID"], ConfigurationManager.AppSettings["ApiTransactionKey"], /*Session["UserEmail"].ToString(),*/ PaymentGatewayVM);

            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.messages.message != null)
                {
                    bool TransResult = _PaymentGatewayService.SaveCardDetails(new CardDetails
                    {
                        CardHolderName = PaymentGatewayVM.CardHolderName,
                        CardNumber = PaymentGatewayVM.DBCardNumber,
                        //CardNumber = PaymentGatewayVM.CardNumber,
                        ExpiryMonth = PaymentGatewayVM.Month,
                        ExpiryYear = PaymentGatewayVM.Year,
                        CustomerProfileId = response.customerProfileId,
                        PaymentProfileId = response.customerPaymentProfileIdList[0],
                        ShippingProfileId = response.customerShippingAddressIdList[0],
                        //CardType = response.,
                        //change for user to userloginid
                        CreatedBy = Convert.ToInt32(Session["UserLoginID"]),
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        CVV= PaymentGatewayVM.CVV

                    });

                    if (TransResult)
                    {
                        return Json(new { result = true, Message = "Transaction succeded" });
                    }
                    else
                    {
                        return Json(new { result = false, Message = "Transaction Failed" });
                    }
                }
                else
                {
                    return Json(new { result = false, Message = "Transaction Failed" });
                }
            }
            else
            {
                return Json(new { result = false, Message = "Transaction Failed" });
            }
        }

        //change to user login id for user
        
        public ActionResult GetCardList(decimal Videorate, string Productname, decimal ProductID,int SubCatID)
        {
            List<CardListVM> ListVM = new List<CardListVM>();
            var lst = _PaymentGatewayService.GetCardDetails(Convert.ToInt32(Session["UserLoginID"]));
            foreach (var card in lst)
            {
                ListVM.Add(new CardListVM
                {
                    ProductId= ProductID,
                    ProductName = Productname,
                    CustomerFName = Convert.ToString(Session["UserNamee"]),
                    createby = Convert.ToInt32(Session["UserLoginID"]),
                    CardNumber = card.CardNumber,
                    CustomerProfileId = card.CustomerProfileId,
                    CustomerPaymentProfileId = card.PaymentProfileId,
                    Amount = Videorate,
                    CardHolderName = card.CardHolderName,
                    CreatedDate = DateTime.Now,
                    CardType = card.CardType,
                    Month = card.ExpiryMonth,
                    Year = card.ExpiryYear,
                    SubCatID=SubCatID
                }); 
            }
            return View("CardList", ListVM);
        }
        //Video seprately data save method
        public ActionResult GetCardMapping(decimal Videorate, string Productname, int ProductID, int SubCatID,int VideoID)
        {
            List<CardListVM> ListVM = new List<CardListVM>();
            var lst = _PaymentGatewayService.GetCardDetails(Convert.ToInt32(Session["UserLoginID"]));
            foreach (var card in lst)
            {
                ListVM.Add(new CardListVM
                {
                     
                    VideoId = VideoID,
                    ProductId = ProductID,
                    ProductName = Productname,
                    CustomerFName = Convert.ToString(Session["UserNamee"]),
                    createby = Convert.ToInt32(Session["UserLoginID"]),
                    CardNumber = card.CardNumber,
                    CustomerProfileId = card.CustomerProfileId,
                    CustomerPaymentProfileId = card.PaymentProfileId,
                    Amount = Videorate,
                    CardHolderName = card.CardHolderName,
                    CreatedDate = DateTime.Now,
                    CardType = card.CardType,
                    Month = card.ExpiryMonth,
                    Year = card.ExpiryYear,
                    SubCatID = SubCatID,

                }) ;
            }
            return View("CardList", ListVM);
        }
        [HttpPost]
        public ActionResult MakePaymentUsingProfile(CardListVM CardListVM)
        {
            bool TransResult=false;
            createTransactionResponse response = PaymentGatewayUtility.ChargeACustomerProfile(ConfigurationManager.AppSettings["ApiLoginID"], ConfigurationManager.AppSettings["ApiTransactionKey"], CardListVM);
            
            if (response != null)
            { 
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    var res = _PaymentGatewayService.GetUserVideosList().ToList();
                    List<ProductVideoViewModel> productVideoViewModel = new List<ProductVideoViewModel>();
                    foreach (var pvideo in res)
                    {
                        if(pvideo.ProductId== CardListVM.ProductId)
                        {
                             TransResult = _PaymentGatewayService.SaveTransactionDetails(new CardTransactionDetails
                            {
                                ProductID = CardListVM.ProductId,
                                ProductName = CardListVM.ProductName,
                                CardHolderName = CardListVM.CardHolderName,
                                CardNumber = CardListVM.CardNumber,
                                Expiration = CardListVM.MonthAndYear,
                                CVV = CardListVM.CVV,
                                ResultCode = response.messages.resultCode.ToString(),
                                Message = response.messages.message[0].text,
                                AccountType = response.transactionResponse.accountType,
                                TransId = response.transactionResponse.transId,
                                ResponseCode = Convert.ToInt32(response.transactionResponse.responseCode),
                                CreatedDate = DateTime.Now,
                                //change for user to userloginid
                                CreatedBy = Convert.ToInt32(Session["UserLoginID"]),
                                Amount = CardListVM.Amount,
                                CustomerFullName = CardListVM.CustomerFName,
                                CategoryID = CardListVM.SubCatID,
                                VideoID = pvideo.Id,
                            });
                        }
                        TransResult = false;
                    }
                    if (TransResult)
                    {
                        TempData["PaymentStatus"] = true;
                        return Json(new { result = true, Message = response.messages.message[0].text });
                    }
                    else
                    {
                        TempData["PaymentStatus"] = true;
                        return Json(new { result = true, Message = response.messages.message[0].text });
                    }
                }
                else
                {
                    TempData["PaymentStatus"] = false;
                    return Json(new { result = false, Message = response.messages.message[0].text });
                }
        }
        else
        {
            return Json(new { result = false, Message = "Transaction Failed" });
        }
            //if (response != null)
            //{
            //    if (response.messages.resultCode == messageTypeEnum.Ok)
            //    {
            //        if (response.transactionResponse.messages != null)
            //        {
            //            return Json(new { result = true, Message = "Transaction Successful" });
            //        }
            //        else
            //        {
            //            return Json(new { result = false, Message = "Transaction Failed" });
            //        }
            //    }
            //    else
            //    {
            //        return Json(new { result = false, Message = "Transaction Failed" });
            //    }
            //}
            //else
            //{
            //    return Json(new { result = false, Message = "Transaction Failed" });
            //}
        }
    }
}
