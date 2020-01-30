using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Repository.Interface;

namespace University.Repository
{
    public class PaymentGatewayRepository : IPaymentGatewayRepository
    {
        public void SaveTransactionDetails(CardTransactionDetail CardTransactionDetail)
        {
            using (var context = new UniversityEntities())
            {
                context.CardTransactionDetails.Add(CardTransactionDetail);
                context.SaveChanges();
            }
        }
    }
}
