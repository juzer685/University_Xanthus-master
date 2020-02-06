using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University.UI.Models;

namespace University.UI.Utilities
{
    public class PaymentGatewayUtility
    {
		public static createTransactionResponse MakePayment(String ApiLoginID, String ApiTransactionKey, PaymentGatewayVM PaymentGatewayVM)
		{
			System.Diagnostics.Debug.WriteLine("Charge Credit Card Sample");

			ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

			// define the merchant information (authentication / transaction id)
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
			{
				name = ApiLoginID,
				ItemElementName = ItemChoiceType.transactionKey,
				Item = ApiTransactionKey,
			};

			var creditCard = new creditCardType
			{
				cardNumber = PaymentGatewayVM.CardNumber,
				expirationDate = PaymentGatewayVM.MonthAndYear, //"0722",
				cardCode = PaymentGatewayVM.CVV.ToString()
			};

			var billingAddress = new customerAddressType
			{
				firstName = "John",
				lastName = "Doe",
				address = "123 My St",
				city = "OurTown",
				zip = "98004"
			};

			//standard api call to retrieve response
			var paymentType = new paymentType { Item = creditCard };

			// Add line Items
			var lineItems = new lineItemType[2];
			lineItems[0] = new lineItemType { itemId = "1", name = "t-shirt", quantity = 2, unitPrice = new Decimal(50.00) };
			lineItems[1] = new lineItemType { itemId = "2", name = "snowboard", quantity = 1, unitPrice = new Decimal(33.45) };

			var transactionRequest = new transactionRequestType
			{
				transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
				amount = 133.45m,
				payment = paymentType,
				billTo = billingAddress,
				lineItems = lineItems
			};

			var request = new createTransactionRequest { transactionRequest = transactionRequest };

			// instantiate the contoller that will call the service
			var controller = new createTransactionController(request);
			controller.Execute();

			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();

			if (response != null)
			{
				if (response.messages.resultCode == messageTypeEnum.Ok)
				{
					if (response.transactionResponse != null)
					{
						System.Diagnostics.Debug.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
					}
				}
				else
				{
					Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
					if (response.transactionResponse != null)
					{
						System.Diagnostics.Debug.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
					}
				}
			}
			else
			{
				Console.WriteLine("Null Response.");
			}
			return response;
		}

        public static createCustomerProfileResponse CreateCustomerProfile(string ApiLoginID, string ApiTransactionKey, string emailId, PaymentGatewayVM PaymentGatewayVM)
        {
            Console.WriteLine("Create Customer Profile Sample");

            // set whether to use the sandbox environment, or production enviornment
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = PaymentGatewayVM.CardNumber,
                expirationDate = PaymentGatewayVM.MonthAndYear
            };

            var bankAccount = new bankAccountType
            {
                accountNumber = "231323342",
                routingNumber = "000000224",
                accountType = bankAccountTypeEnum.checking,
                echeckType = echeckTypeEnum.WEB,
                nameOnAccount = "test",
                bankName = "Bank Of America"
            };

            // standard api call to retrieve response
            paymentType cc = new paymentType { Item = creditCard };
            paymentType echeck = new paymentType { Item = bankAccount };

            List<customerPaymentProfileType> paymentProfileList = new List<customerPaymentProfileType>();
            customerPaymentProfileType ccPaymentProfile = new customerPaymentProfileType();
            ccPaymentProfile.payment = cc;

            customerPaymentProfileType echeckPaymentProfile = new customerPaymentProfileType();
            echeckPaymentProfile.payment = echeck;

            paymentProfileList.Add(ccPaymentProfile);
            paymentProfileList.Add(echeckPaymentProfile);

            List<customerAddressType> addressInfoList = new List<customerAddressType>();
            customerAddressType homeAddress = new customerAddressType();
            homeAddress.address = "10900 NE 8th St";
            homeAddress.city = "Seattle";
            homeAddress.zip = "98006";


            customerAddressType officeAddress = new customerAddressType();
            officeAddress.address = "1200 148th AVE NE";
            officeAddress.city = "NorthBend";
            officeAddress.zip = "92101";

            addressInfoList.Add(homeAddress);
            addressInfoList.Add(officeAddress);


            customerProfileType customerProfile = new customerProfileType();
            customerProfile.merchantCustomerId = "Test CustomerID";
            customerProfile.email = emailId;
            customerProfile.paymentProfiles = paymentProfileList.ToArray();
            customerProfile.shipToList = addressInfoList.ToArray();

            var request = new createCustomerProfileRequest { profile = customerProfile, validationMode = validationModeEnum.none };

            // instantiate the controller that will call the service
            var controller = new createCustomerProfileController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            createCustomerProfileResponse response = controller.GetApiResponse();

            // validate response 
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.messages.message != null)
                    {
                        Console.WriteLine("Success!");
                        Console.WriteLine("Customer Profile ID: " + response.customerProfileId);
                        Console.WriteLine("Payment Profile ID: " + response.customerPaymentProfileIdList[0]);
                        Console.WriteLine("Shipping Profile ID: " + response.customerShippingAddressIdList[0]);
                    }
                }
                else
                {
                    Console.WriteLine("Customer Profile Creation Failed.");
                    Console.WriteLine("Error Code: " + response.messages.message[0].code);
                    Console.WriteLine("Error message: " + response.messages.message[0].text);
                }
            }
            else
            {
                if (controller.GetErrorResponse().messages.message.Length > 0)
                {
                    Console.WriteLine("Customer Profile Creation Failed.");
                    Console.WriteLine("Error Code: " + response.messages.message[0].code);
                    Console.WriteLine("Error message: " + response.messages.message[0].text);
                }
                else
                {
                    Console.WriteLine("Null Response.");
                }
            }

            return response;
        }
    }
}
