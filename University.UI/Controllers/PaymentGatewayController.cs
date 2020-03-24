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
                    Amount = PaymentGatewayVM.Amount,
                    ProductName = PaymentGatewayVM.ProductName,
                    CustomerFullName = PaymentGatewayVM.CustomerFName
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
            List<CardListVM> ListVM = new List<CardListVM>();
            var lst = _PaymentGatewayService.GetCardDetails(Convert.ToInt32(Session["UserLoginID"]));
            foreach (var card in lst)
            {
                ListVM.Add(new CardListVM
                {
                   // ProductId = ProductID,
                   // ProductName = Productname,
                    CustomerFName = Convert.ToString(Session["UserNamee"]),
                    createby = Convert.ToInt32(Session["UserLoginID"]),
                    CardNumber = card.CardNumber,
                    CustomerProfileId = card.CustomerProfileId,
                    CustomerPaymentProfileId = card.PaymentProfileId,
                    //Amount = Videorate,
                    CardHolderName = card.CardHolderName,
                    CreatedDate = DateTime.Now,
                    CardType = card.CardType,
                    Month = card.ExpiryMonth,
                    Year = card.ExpiryYear,
                    //SubCatID = SubCatID,
                    //isProductbuy = isProductbuy,
                });
            }
            //return View("CardList", ListVM);
            //return View("CardDetails", ListVM);
            PaymentGatewayVM paymentGatewayVM = new PaymentGatewayVM();
            paymentGatewayVM.cardListVMs = ListVM;
            //return View("CardDetails", new PaymentGatewayVM());
            return View("CardDetails", paymentGatewayVM);
        }

        private static Random RNG = new Random();

        //change admin email and ID to user when migrating to user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCardDetails(PaymentGatewayVM PaymentGatewayVM)
        {
            var fileId = new System.Text.StringBuilder();
            while (fileId.Length < 16)
            {
                fileId.Append(RNG.Next(10).ToString());
            }
            //var fileId  =Guid.NewGuid();
            //Guid obj = Guid.NewGuid();
            //var fileId = Guid.NewGuid().ToString().Replace("-", "");
            PaymentGatewayVM.GuidString = fileId.ToString();
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
                        CVV = PaymentGatewayVM.CVV

                    });

                    if (TransResult)
                    {
                        return Json(new { result = true, Message = "Card Added succeded" });
                    }
                    else
                    {
                        return Json(new { result = false, Message = "Card Added Failed" });
                    }
                }
                else
                {
                    return Json(new { result = false, Message = "Card Added Failed" });
                }
            }
            else
            {
                return Json(new { result = false, Message = "Card Added Failed" });
            }
        }

        //change to user login id for user

        public ActionResult GetCardList(decimal Videorate, string Productname, decimal ProductID, int SubCatID,bool isProductbuy)
        {
            List<CardListVM> ListVM = new List<CardListVM>();
            var lst = _PaymentGatewayService.GetCardDetails(Convert.ToInt32(Session["UserLoginID"]));
            foreach (var card in lst)
            {
                ListVM.Add(new CardListVM
                {
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
                    isProductbuy= isProductbuy,
                });
            }
            return View("CardList", ListVM);
        }
        //Video seprately data save method
        public ActionResult GetCardMapping(decimal Videorate, string Productname, int ProductID, int SubCatID, int VideoID,bool isbuy)
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
                    isbuy=isbuy

                });
            }
            return View("CardList", ListVM);
        }
        [HttpPost]
        public ActionResult MakePaymentUsingProfile(CardListVM CardListVM)
        {
            bool TransResult = false;
            createTransactionResponse response = PaymentGatewayUtility.ChargeACustomerProfile(ConfigurationManager.AppSettings["ApiLoginID"], ConfigurationManager.AppSettings["ApiTransactionKey"], CardListVM);

            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    var res = _PaymentGatewayService.GetUserVideosList().ToList();
                    //List<CardTransactionDetailsMappings> cardTransactionDetailsMappings = new List<CardTransactionDetailsMappings>();
                    var res1= _PaymentGatewayService.GetCardTransDeatilsMapp().ToList();
                    
                    ////List<ProductVideoViewModel> productVideoViewModel = new List<ProductVideoViewModel>();
                    foreach (var pvideo in res)
                    {
                        if (pvideo.Id == CardListVM.VideoId && CardListVM.isbuy== true)
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
                                isbuy= CardListVM.isbuy,
                            });
                        }
                        else if (pvideo.ProductId == CardListVM.ProductId && CardListVM.isProductbuy == true)
                        {
                            var data = res1.Where(c => c.VideoID == pvideo.Id).FirstOrDefault();
                            //check if null
                            if(data == null)
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
                                    isProductbuy = CardListVM.isProductbuy,
                                });
                            }
                            //else
                            //{
                            //    break;
                            //}
                        }
                        // TransResult = false;
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

        }

    }
}
