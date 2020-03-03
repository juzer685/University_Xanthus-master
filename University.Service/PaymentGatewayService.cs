using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Data.CustomEntities;
using University.Repository.Interface;
using University.Service.Interface;

namespace University.Service
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private readonly IPaymentGatewayRepository _PaymentGatewayRepository;
        public PaymentGatewayService(IPaymentGatewayRepository IPaymentGatewayRepository)
        {
            _PaymentGatewayRepository = IPaymentGatewayRepository;
        }

        public bool SaveTransactionDetails(CardTransactionDetails CardTransactionDetail)
        {
            return _PaymentGatewayRepository.SaveTransactionDetails(CardTransactionDetail);
        }

        public bool SaveCardDetails(CardDetails CardDetails)
        {
            return _PaymentGatewayRepository.SaveCardDetails(CardDetails);
        }

        public List<CardDetails> GetCardDetails(int UserId)
        {
            return _PaymentGatewayRepository.GetCardDetails(UserId);
        }

        public IEnumerable<ProductVideos> GetUserVideosList()
        {
            return _PaymentGatewayRepository.GetUserVideosList();
        }

    }
}
