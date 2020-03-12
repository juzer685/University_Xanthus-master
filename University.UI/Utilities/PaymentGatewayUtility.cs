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

        //To make payment using credit card
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
				amount = PaymentGatewayVM.Amount,
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

        //To create customer profile on server
        public static createCustomerProfileResponse CreateCustomerProfile(string ApiLoginID, string ApiTransactionKey ,PaymentGatewayVM PaymentGatewayVM)
        {
            Console.WriteLine("Create Customer Profile Sample");
            // set whether to use the sandbox environment, or production enviornment
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
                
               // mobileDeviceId="34567"
            };
            

            var creditCard = new creditCardType
            {
                cardNumber = PaymentGatewayVM.CardNumber,
                expirationDate = PaymentGatewayVM.MonthAndYear,
                cardCode = PaymentGatewayVM.CVV.ToString(),
                //cryptogram = Guid.NewGuid().ToString()
                // Set the token specific info
                //          isPaymentToken = true,
                // Set this to the value of the cryptogram received from the token provide
                //cryptogram = "EjRWeJASNFZ4kBI0VniQEjRWeJA=",
                //tokenRequestorName = "CHASE_PAY",
                //tokenRequestorId = "12345678901",
                //tokenRequestorEci = "07"

            };

            var bankAccount = new bankAccountType
            {
                accountNumber = "231323",
                routingNumber = "000000225",
                accountType = bankAccountTypeEnum.checking,
                echeckType = echeckTypeEnum.WEB,
                nameOnAccount = "tested",
                bankName = "Bank Of India"
                
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
           
            customerProfile.merchantCustomerId = "2CLINC555557556tech";
            //customerProfile.description = "emailIdssshtitest";
            customerProfile.paymentProfiles = paymentProfileList.ToArray();
            customerProfile.shipToList = addressInfoList.ToArray();
            
            //customerProfile.su

            var request = new createCustomerProfileRequest { profile = customerProfile, validationMode = validationModeEnum.none };
            //request.refId= 1361101257555
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

        //Get saved customer profile from server
        public static getCustomerProfileResponse GetCustomerProfile(String ApiLoginID, String ApiTransactionKey, string customerProfileId,PaymentGatewayVM PaymentGatewayVM)
        {
            Console.WriteLine("Get Customer Profile sample");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var request = new getCustomerProfileRequest();
            request.customerProfileId = customerProfileId;

            // instantiate the controller that will call the service
            var controller = new getCustomerProfileController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                Console.WriteLine(response.messages.message[0].text);
                Console.WriteLine("Customer Profile Id: " + response.profile.customerProfileId);

                if (response.subscriptionIds != null && response.subscriptionIds.Length > 0)
                {
                    Console.WriteLine("List of subscriptions : ");
                    for (int i = 0; i < response.subscriptionIds.Length; i++)
                        Console.WriteLine(response.subscriptionIds[i]);
                }

            }
            else if (response != null)
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
                                  response.messages.message[0].text);
            }

            return response;
        }

        //Charge a saved customer profile directly on server by sending the profile Id
        public static createTransactionResponse ChargeACustomerProfile(string ApiLoginID, string ApiTransactionKey,CardListVM cardListVM)
        {
            Console.WriteLine("Charge Customer Profile");
           
            

                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            //create a customer payment profile
            customerProfilePaymentType profileToCharge = new customerProfilePaymentType();
            profileToCharge.customerProfileId = cardListVM.CustomerProfileId;
            profileToCharge.paymentProfile = new paymentProfile { paymentProfileId = cardListVM.CustomerPaymentProfileId };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // refund type
                amount = cardListVM.Amount,
                profile = profileToCharge
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the collector that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                        Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                        Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                        Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                        Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                    else
                    {
                        Console.WriteLine("Failed Transaction.");
                        if (response.transactionResponse.errors != null)
                        {
                            Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                            Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed Transaction.");
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        Console.WriteLine("Error Code: " + response.messages.message[0].code);
                        Console.WriteLine("Error message: " + response.messages.message[0].text);
                    }
                }
            }
            else
            {
                Console.WriteLine("Null Response.");
            }

            return response;
        }

        //Create a new customer payment profile on server
        public static ANetApiResponse CreateNewPaymentProfile(string ApiLoginID, string ApiTransactionKey, string customerProfileId, PaymentGatewayVM PaymentGatewayVM)
        {
            Console.WriteLine("Create Customer Payment Profile Sample");

            // set whether to use the sandbox environment, or production enviornment
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            //var bankAccount = new bankAccountType
            //{
            //    accountNumber = "01245524321",
            //    routingNumber = "000000204",
            //    accountType = bankAccountTypeEnum.checking,
            //    echeckType = echeckTypeEnum.WEB,
            //    nameOnAccount = "test",
            //    bankName = "Bank Of America"
            //};
            var creditCard = new creditCardType
            {
                cardNumber = PaymentGatewayVM.CardNumber,
                expirationDate = PaymentGatewayVM.MonthAndYear,
                cardCode = PaymentGatewayVM.CVV.ToString()
            };
            paymentType echeck = new paymentType { Item = creditCard };

            var billTo = new customerAddressType
            {
                firstName = "John",
                lastName = "Snow"
            };
            customerPaymentProfileType echeckPaymentProfile = new customerPaymentProfileType();
            echeckPaymentProfile.payment = echeck;
            echeckPaymentProfile.billTo = billTo;

            var request = new createCustomerPaymentProfileRequest
            {
                customerProfileId = customerProfileId,
                paymentProfile = echeckPaymentProfile,
                validationMode = validationModeEnum.none
            };

            // instantiate the controller that will call the service
            var controller = new createCustomerPaymentProfileController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            createCustomerPaymentProfileResponse response = controller.GetApiResponse();

            // validate response 
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.messages.message != null)
                    {
                        Console.WriteLine("Success! Customer Payment Profile ID: " + response.customerPaymentProfileId);
                    }
                }
                else
                {
                    Console.WriteLine("Customer Payment Profile Creation Failed.");
                    Console.WriteLine("Error Code: " + response.messages.message[0].code);
                    Console.WriteLine("Error message: " + response.messages.message[0].text);
                    if (response.messages.message[0].code == "E00039")
                    {
                        Console.WriteLine("Duplicate Payment Profile ID: " + response.customerPaymentProfileId);
                    }
                }
            }
            else
            {
                if (controller.GetErrorResponse().messages.message.Length > 0)
                {
                    Console.WriteLine("Customer Payment Profile Creation Failed.");
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
