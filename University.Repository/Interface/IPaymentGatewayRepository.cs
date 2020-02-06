using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;

namespace University.Repository.Interface
{
    public interface IPaymentGatewayRepository
    {
        bool SaveTransactionDetails(CardTransactionDetails CardTransactionDetail);
        bool SaveCardDetails(CardDetails CardDetails);
    }
}
