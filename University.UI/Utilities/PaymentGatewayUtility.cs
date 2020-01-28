using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Utilities
{
    public class PaymentGatewayUtility
    {
		public static void Run(String ApiLoginID, String ApiTransactionKey)
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
				cardNumber = "4111111111111111",
				expirationDate = "0722"
			};

			//standard api call to retrieve response
			var paymentType = new paymentType { Item = creditCard };

			var transactionRequest = new transactionRequestType
			{
				transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
				amount = 133.45m,
				payment = paymentType
			};

			var request = new createTransactionRequest { transactionRequest = transactionRequest };

			// instantiate the contoller that will call the service
			var controller = new createTransactionController(request);
			controller.Execute();

			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();

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
	}
}