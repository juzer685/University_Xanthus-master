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
        public bool SaveTransactionDetails(CardTransactionDetails CardTransactionDetail)
        {
            try
            {
                using (var context = new UniversityEntities())
                {
                    context.CardTransactionDetails.Add(CardTransactionDetail);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
