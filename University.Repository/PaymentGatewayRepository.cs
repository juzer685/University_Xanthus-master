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

        public bool SaveCardDetails(CardDetails CardDetails)
        {
            try
            {
                using (var context = new UniversityEntities())
                {
                    context.CardDetails.Add(CardDetails);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<CardDetails> GetCardDetails(int UserId)
        {
            try
            {
                using (var context = new UniversityEntities())
                {
                    return context.CardDetails.Where(x => x.CreatedBy == UserId && x.IsDeleted==false).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<CardDetails>();
            }
        }
        
    }
}
