using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;

namespace University.Service.Interface
{
    public interface IPaymentGatewayService
    {
        void SaveTransactionDetails(CardTransactionDetail CardTransactionDetail);
    }
}
