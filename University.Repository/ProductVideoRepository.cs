using University.Data;
using University.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace University.Repository
{
    public class ProductVideoRepository : IProductVideoRepository
    {
        public List<ProductVideos> GetProductVideoList()
        {
            using (var context = new UniversityEntities())
            {

                // int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                return context.ProductVideos.Where(x => x.IsDeleted != true && x.Title != null).ToList();
            }
        }
        public IEnumerable<ProductVideoModel> GetUserVideosList()
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
                           //join ctd in context.CardTransactionDetails.Where(y => y.CreatedBy == UserID && y.Message == "Successful.")
                           //on pv.ProductId equals ctd.ProductID
                           orderby pv.CreatedDate
                           select new ProductVideoModel 
                           {
                               Id=pv.Id,
                               Title=pv.Title,
                               Decription=pv.Decription,
                               VideoURL=pv.VideoURL,
                               ProductId = pv.ProductId,
                               IsPaid = false,
                               VideoRate= pv.VideoRate ??0,
                               SubcatId=c.Id
                           }).ToList();

                //var PaidVideodata = (from c in res
                //            from d in context.CardTransactionDetails
                //            where c.ProductId == d.ProductID
                //            && d.CreatedBy == UserID
                //            select new ProductVideoModel 
                //            {
                //                Id = c.Id,
                //                Title = c.Title,
                //                Decription = c.Decription,
                //                VideoURL = c.VideoURL,
                //                ProductId = c.ProductId,
                //                IsPaid = true
                //            }).ToList();
                
                //get paid video list if click on product buy button
                //get paid video list if click on video buy button
                //remaing video list which is not paid

                //TODO:if click on each video buy button then show product value - that each video value
                //if user click on product buy button and if some video already added in database then first remove and then add product 

                //get videos list from res which not paid video
                List <ProductVideoModel> productvideo = new List<ProductVideoModel>();
                // productvideo = res.Except(PaidVideodata).ToList();
                foreach (var prodvideo in res)
                {
                    //get videos list which product exist in card transaction details table with ispaid falg true
                    if (context.CardTransactionDeatilsMapping.Where(c=>c.VideoID==prodvideo.Id && c.UserID == UserID).Count()!=0){
                        productvideo.Add(new ProductVideoModel
                        {
                            Id = prodvideo.Id,
                            Title = prodvideo.Title,
                            Decription = prodvideo.Decription,
                            VideoURL = prodvideo.VideoURL,
                            IsPaid = true,
                            ProductId=prodvideo.ProductId,
                            VideoRate=prodvideo.VideoRate,
                            SubcatId = prodvideo.SubcatId
                        });
                    }
                    else
                    {
                        productvideo.Add(new ProductVideoModel
                        {
                            Id = prodvideo.Id,
                            Title = prodvideo.Title,
                            Decription = prodvideo.Decription,
                            VideoURL = prodvideo.VideoURL,
                            IsPaid = false,
                            ProductId = prodvideo.ProductId,
                            VideoRate = prodvideo.VideoRate,
                            SubcatId = prodvideo.SubcatId
                        });
                    }
                   
                    //var title = prodvideo.Title;
                    //var des = prodvideo.Decription;
                    //var ProductId = prodvideo.ProductId;
                    //var VideoURL = prodvideo.VideoURL;
                }
                return productvideo;
            }
        }



    }
}
