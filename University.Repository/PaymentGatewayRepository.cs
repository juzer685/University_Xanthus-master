using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using University.Data;
using University.Data.CustomEntities;
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
                    
                    List<CardTransactionDeatilsMapping> cardTransactionDeatilsMapping = new List<CardTransactionDeatilsMapping>();
                    //cardTransactionDeatilsMapping.TransactionID = CardTransactionDetail.TransId;
                    context.CardTransactionDeatilsMapping.Add(new CardTransactionDeatilsMapping
                    {
                        TransactionID = CardTransactionDetail.TransId,
                        UserID = CardTransactionDetail.CreatedBy,
                        CategoryID = CardTransactionDetail.CategoryID,
                        ProductID = CardTransactionDetail.ProductID,
                        VideoID= CardTransactionDetail.VideoID,
                        IsDeleted = false
                    }); 
                    context.SaveChanges();
                    //ProductEntity productEntity = new ProductEntity();
                    //context.Product.Add(new  Product{ 
                    //    TransactionID=CardTransactionDetail.TransId,
                    //    Id=CardTransactionDetail.ProductID??0
                    //});
                   // productEntity.TransactionId = CardTransactionDetail.TransId;
                   // productEntity.Id = CardTransactionDetail.ProductID ??0;
                 }
                    //productvideo.SaveChanges();
                    return true;
                
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

        public IEnumerable<ProductVideos> GetUserVideosList()
        {
            using (var context = new UniversityEntities())
            {
                int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                //int AssocitedCustID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                var res = (from l in context.Login_tbl.Where(y => y.IsDeleted != true && y.ID == UserID)
                           join cm in context.CategoryUserMapping.Where(y => y.IsDeleted != true && y.UserID == UserID)
                           on l.ID equals cm.UserID
                           join c in context.SubCategoryMaster.Where(y => y.IsDeleted != true)
                           on cm.CategoryID equals c.Id
                           join p in context.Product.Where(y => y.IsDeleted != true)
                           on c.Id equals p.SubCategoryId
                           join pv in context.ProductVideos.Where(y => y.IsDeleted != true)
                           on p.Id equals pv.ProductId
                           orderby pv.CreatedDate
                           select new
                           {
                               pv.Id,
                               pv.Title,
                               pv.Decription,
                               pv.ProductId,
                               pv.VideoURL,
                              

                           }

                           ).ToList();
                List<ProductVideos> productvideo = new List<ProductVideos>();
                foreach (var prodvideo in res)
                {
                    productvideo.Add(new ProductVideos
                    {
                        Id = prodvideo.Id,
                        Title = prodvideo.Title,
                        Decription = prodvideo.Decription,
                        VideoURL = prodvideo.VideoURL,
                        ProductId= prodvideo.ProductId,
                       

                    });
                    //var title = prodvideo.Title;
                    //var des = prodvideo.Decription;
                    //var ProductId = prodvideo.ProductId;
                    //var VideoURL = prodvideo.VideoURL;
                }
                return productvideo;
            }
        }

        public IEnumerable<CardTransactionDetailsMappings> GetCardTransDeatilsMapp()
        {
            using (var context = new UniversityEntities())
            {
                int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);

                var res = (from ctd in context.CardTransactionDeatilsMapping.Where(c => c.UserID == UserID && c.IsDeleted == false)
                           join v in context.ProductVideos
                           on ctd.VideoID equals v.Id
                           join l in context.Login_tbl.Where(y => y.IsDeleted != true)
                           on ctd.UserID equals l.ID
                           select new CardTransactionDetailsMappings
                           {
                               CTMID = ctd.CTMID,
                               VideoID = ctd.VideoID,
                               UserID = ctd.UserID,
                               //IsPaid = ctd.IsPaid,
                               CategoryID = ctd.CategoryID,
                               TransactionID = ctd.TransactionID,
                               ProductID = ctd.ProductID,
                               VideoRate = v.VideoRate ?? 0
                           }).ToList();
               
                return res;
            }
        }
    }
}
