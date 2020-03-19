using University.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Data.CustomEntities;
using University.Repository.Interface;
using System.Web;

namespace University.Repository
{
    public class HomeRepository : IHomeRepository
    {
        public IEnumerable<HomeSlider> GetHomeSliderList()
        {
            using (var context = new UniversityEntities())
            {
                int AdminID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                if (AdminID != 0)
                {

                    return context.HomeSlider.Include("Product").Where(y => y.IsDeleted != true).OrderByDescending(y => y.CreatedDate).ToList();
                }
                else
                {
                    int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                    return context.HomeSlider.Include("Product").Where(y => y.IsDeleted != true).OrderByDescending(y => y.CreatedDate).ToList();

                }
                //else
                //{
                //    return context.HomeSlider.Include("Product").Where(y => y.IsDeleted != true).OrderByDescending(y => y.CreatedDate).ToList();
                //}

            }
        }
        public HomeSlider GetHomeSlider(int id)
        {
            using (var context = new UniversityEntities())
            {
                return context.HomeSlider.Include("Product").FirstOrDefault(y => y.Id == id && y.IsDeleted != true);
            }
        }

        public bool DeleteHomeSlider(int id)
        {
            using (var context = new UniversityEntities())
            {
                var slider = context.HomeSlider.FirstOrDefault(y => y.Id == id && y.IsDeleted != true);
                if (slider != null)
                {
                    slider.IsDeleted = true;
                    slider.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                return true;
            }
        }

        public bool AddOrUpdateHomeSlider(HomeSlider model)
        {
            using (var context = new UniversityEntities())
            {
                int AdminID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);

                var slider = context.HomeSlider.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);

                if (slider != null)
                {
                    slider.ProductId = model.ProductId;
                    slider.AssocitedCustID = AdminID;
                    slider.TextDescription = model.TextDescription;
                    slider.Link = model.Link;
                    slider.ImageALT = model.ImageALT;
                    slider.IsDeleted = false;
                    if (!string.IsNullOrWhiteSpace(model.ImageURL))
                    {
                        slider.ImageURL = model.ImageURL;
                    }
                    slider.UpdatedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                else
                {
                    model.AssocitedCustID = AdminID;
                    model.IsDeleted = false;
                    model.CreatedDate = DateTime.UtcNow;
                    context.HomeSlider.Add(model);
                    context.SaveChanges();
                }
                return true;


            }
        }

        public List<HomeSlider> GetHomeSliders()
        {
            using (var context = new UniversityEntities())
            {
                return context.HomeSlider.Include("Product").Where(y => y.IsDeleted != true).ToList();
            }
        }

        public HomeBanner GetHomeBanner()
        {
            using (var context = new UniversityEntities())
            {
                int AssocitedCustID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                if (AssocitedCustID != 0)
                {

                    return context.HomeBanner.Where(y => y.IsDeleted != null && y.IsDeleted != true).OrderByDescending(y => y.UpdatedDate).FirstOrDefault();

                }
                else
                {
                    int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);

                    return context.HomeBanner.Where(y => y.IsDeleted != null && y.IsDeleted != true).OrderByDescending(y => y.UpdatedDate).FirstOrDefault();
                }


            }
        }

        public bool AddOrUpdateHomeBanner(HomeBanner model)
        {
            using (var context = new UniversityEntities())
            {
                int AssocitedCustID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);

                var banner = context.HomeBanner.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);

                if (banner != null)
                {
                    banner.Description = model.Description;

                    banner.Title = model.Title;
                    banner.ImageALT = model.ImageALT;
                    banner.LinkTo = model.LinkTo;
                    banner.AssocitedCustID = AssocitedCustID;
                    if (!string.IsNullOrWhiteSpace(model.ImageURL))
                    {
                        banner.ImageURL = model.ImageURL;
                    }
                    banner.IsDeleted = false;
                    banner.UpdatedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                else
                {
                    model.AssocitedCustID = AssocitedCustID;
                    model.CreatedDate = DateTime.UtcNow;
                    model.IsDeleted = false;
                    context.HomeBanner.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
        }

        public bool DeleteHomeBanner(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                var slider = context.HomeBanner.FirstOrDefault(y => y.Id == id && y.IsDeleted != true);
                if (slider != null)
                {
                    slider.IsDeleted = true;
                    slider.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                return true;
            }
        }
        //public List<SmartSerach_Result> SmartSearch(string freeText)
        //{
        //    using (var context = new IPSU_devEntities())
        //    {
        //        return context.SmartSerach(freeText).ToList();
        //    }
        //}

        #region FAQ
        public IEnumerable<FAQ> GetFAQList()
        {
            using (var context = new UniversityEntities())
            {
                int AdminID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                if (AdminID != 0)
                {
                    var res = context.FAQ.Where(y => y.IsDeleted == false && y.AssocitedCustID == AdminID).OrderByDescending(y => y.CreatedDate).ToList();
                    return res.Select(y => new FAQ()
                    {
                        Id = y.Id,
                        Question = y.Question,
                        Answer = y.Answer,
                        AssocitedCustID = AdminID,
                        FAQDoc = null
                    }).OrderByDescending(y => y.CreatedDate).ToList();
                }
                else
                {
                    int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                    var res = context.FAQ.Where(y => y.IsDeleted != true).OrderByDescending(y => y.CreatedDate).ToList();
                    return res.Select(y => new FAQ()
                    {
                        Id = y.Id,
                        Question = y.Question,
                        Answer = y.Answer,
                        //AssocitedID = y.AssocitedID,
                        FAQDoc = null
                    }).OrderByDescending(y => y.CreatedDate).ToList();
                }

            }
        }

        public FAQ GetFAQ(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                return context.FAQ.FirstOrDefault(y => y.Id == id && y.IsDeleted != true);
            }
        }
        public bool DeleteFAQ(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                var faq = context.FAQ.FirstOrDefault(y => y.Id == id && y.IsDeleted != true);
                if (faq != null)
                {
                    faq.IsDeleted = true;
                    faq.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                return true;
            }
        }

        public bool AddOrUpdateFAQ(FAQ model)
        {
            using (var context = new UniversityEntities())
            {
                int AdminID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                var faq = context.FAQ.Include("FAQDoc").FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
                if (AdminID != 0)
                {
                    if (faq != null)
                    {
                        faq.Question = model.Question;
                        faq.Answer = model.Answer;
                        faq.AssocitedCustID = AdminID;
                        faq.IsDeleted = false;


                        if (faq.FAQDoc.Count > 0)
                        {
                            foreach (var faqdoc in faq.FAQDoc)
                                faq.FAQDoc.Remove(faqdoc);
                        }
                        if (model.FAQDoc.Count > 0)
                        {
                            foreach (var faqdoc in model.FAQDoc)
                                faq.FAQDoc.Add(faqdoc);
                        }
                        //if (!string.IsNullOrWhiteSpace(model.ImageURL))
                        //{
                        //    faq.ImageURL = model.ImageURL;
                        //}
                        faq.UpdatedDate = DateTime.UtcNow;
                        context.SaveChanges();
                    }
                    else
                    {
                        model.CreatedDate = DateTime.UtcNow;
                        model.IsDeleted = false;
                        model.AssocitedCustID = AdminID;
                        context.FAQ.Add(model);
                        context.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    if (faq != null)
                    {
                        faq.Question = model.Question;
                        faq.Answer = model.Answer;
                        // faq.AssocitedID = model.AssocitedID;


                        if (faq.FAQDoc.Count > 0)
                        {
                            foreach (var faqdoc in faq.FAQDoc)
                                faq.FAQDoc.Remove(faqdoc);
                        }
                        if (model.FAQDoc.Count > 0)
                        {
                            foreach (var faqdoc in model.FAQDoc)
                                faq.FAQDoc.Add(faqdoc);
                        }
                        //if (!string.IsNullOrWhiteSpace(model.ImageURL))
                        //{
                        //    faq.ImageURL = model.ImageURL;
                        //}
                        faq.UpdatedDate = DateTime.UtcNow;
                        context.SaveChanges();
                    }
                    else
                    {
                        model.CreatedDate = DateTime.UtcNow;
                        context.FAQ.Add(model);
                        context.SaveChanges();
                    }
                    return true;
                }
            }
        }

        #endregion
        #region Rencent Viewed product
        public void AddProductToRecentViewed(RecentVisitedProduct model)
        {
            using (var context = new UniversityEntities())
            {
                try
                {
                    model.ViewedDate = DateTime.UtcNow;
                    context.RecentVisitedProduct.Add(model);
                    context.SaveChanges();
                }
                catch (Exception ex) { }
            }
        }
        public RecentVisitedProduct GetRecentVisitedProduct(int Id)
        {
            RecentVisitedProduct model = new RecentVisitedProduct();
            using (var context = new UniversityEntities())
            {
                model = context.RecentVisitedProduct.Where(x => x.UserId == Id).OrderByDescending(x => x.ViewedDate).FirstOrDefault();

                if (model == null)
                {
                    return new RecentVisitedProduct();
                }
                else
                {
                    model.Product = context.Product.Where(x => x.Id == model.ProductId).FirstOrDefault();
                    return model;
                }
            }

        }

        public IEnumerable<ProductEntity> ListproductbyUserId()
        {
            //decimal videosSum;
            using (var context = new UniversityEntities())
            {
                int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                var res1 = (from l in context.Login_tbl.Where(y => y.IsDeleted == false && y.ID == UserID)
                            join cm in context.CategoryUserMapping.Where(y => y.IsDeleted == false && y.UserID == UserID)
                            on l.ID equals cm.UserID
                            join c in context.SubCategoryMaster.Where(y => y.IsDeleted == false)
                            on cm.CategoryID equals c.Id
                            join p in context.Product.Where(y => y.IsDeleted == false)
                            on c.Id equals p.SubCategoryId
                            //join ctdm in context.CardTransactionDeatilsMapping
                            //on p.Id equals ctdm.ProductID
                            join pv in context.ProductVideos//.Where(y => y.IsDeleted == false )
                            on p.Id equals pv.ProductId
                            orderby p.CreatedDate
                            //group pv by pv.ProductId into g
                            group pv by pv.ProductId into g
                            select new 
                            {
                                g.Key, 
                                videosSum = g.ToList().Sum(x => x.VideoRate),
                                //productvideolist=g.ToList().Select(x=>x.ProductId)
                                //productvideoslist=g.ToList().Where(x=>x.ProductId==)
                               
                            });
                //select new { ProductId = pvs.Key, videos = pvs.ToList() });
                            //var Productlst = new List<ProductEntity>();
                            //foreach (var videos in res1)
                            //{
                            //    Productlst.Add(new ProductEntity
                            //    {
                            //        Id = videos.Key,
                            //        VideoRateSum = videos.videosSum
                            //    });
                            //}
                //int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                var res = (from l in context.Login_tbl.Where(y => y.IsDeleted == false && y.ID == UserID)
                           join cm in context.CategoryUserMapping.Where(y => y.IsDeleted == false && y.UserID == UserID)
                           on l.ID equals cm.UserID
                           join c in context.SubCategoryMaster.Where(y => y.IsDeleted == false)
                           on cm.CategoryID equals c.Id
                           join p in context.Product.Where(y => y.IsDeleted == false )
                           on c.Id equals p.SubCategoryId
                           //join pv in context.ProductVideos
                           //on p.Id equals pv.ProductId
                           //join pp in context.Product.Where(y => y.IsDeleted == false)
                           //on pv.ProductId equals pp.Id
                           orderby p.CreatedDate
                           select new ProductEntity()
                           {
                               Id = p.Id,
                               CreatedBy = p.CreatedBy,
                               CreatedDate = p.CreatedDate,
                               DeletedBy = p.DeletedBy,
                               DeletedDate = p.DeletedDate,
                               Description = p.Description,
                               ImageURL = p.ImageURL,
                               IsDeleted = p.IsDeleted,
                               SubCategoryId = p.SubCategoryId,
                               Title = p.Title,
                               UpdatedBy = p.UpdatedBy,
                               UpdatedDate = p.UpdatedDate,
                              // VideoIds= context.ProductVideos.Where(x=>x.IsDeleted!=true && x.ProductId==p.Id).Select(x=>x.Id).FirstOrDefault(),
                               CardTransactionDetails=context.CardTransactionDetails.Where(x=>x.CreatedBy== UserID).ToList(),
                               Categorymapp = context.CategoryUserMapping.Where(x => x.IsDeleted != true && x.UserID == UserID).ToList(),
                               ProductVideos = context.ProductVideos.Where(x => x.IsDeleted != true).ToList(),
                               ProductDocuments = context.ProductDocuments.Where(x => x.IsDeleted != true).ToList()
                           }).ToList();
                //foreach(var kk in res)
                //{
                //    if(kk.Categorymapp.Select(x=>x.UserID).FirstOrDefault() == UserID)
                //    {
                //        List<Product> hh = new List<Product>();
                //        hh = kk;
                //    }
                //}
                foreach(var item1 in res)
                {
                    item1.ProductVideos = context.ProductVideos.Where(y => y.IsDeleted != true).ToList();
                }

                foreach (var item in res)
                {
                  
                    item.VideoRateSum = res1.Where(x => x.Key == item.Id).Select(x => x.videosSum ?? 0).FirstOrDefault(); 
                }


                return res;
            }
        }
        
        #endregion

            //get video List on Home page
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
                               Id = pv.Id,
                               Title = pv.Title,
                               Decription = pv.Decription,
                               VideoURL = pv.VideoURL,
                               ProductId = pv.ProductId,
                               IsPaid = false,
                               VideoRate = pv.VideoRate ?? 0,
                               SubcatId = c.Id
                           }).ToList();

                //get paid video list if click on product buy button
                //get paid video list if click on video buy button
                //remaing video list which is not paid

                //TODO:if click on each video buy button then show product value - that each video value
                //if user click on product buy button and if some video already added in database then first remove and then add product 

                //get videos list from res which not paid video
                List<ProductVideoModel> productvideo = new List<ProductVideoModel>();
                // productvideo = res.Except(PaidVideodata).ToList();
                foreach (var prodvideo in res)
                {
                    //get videos list which product exist in card transaction details table with ispaid falg true
                    //if (context.CardTransactionDeatilsMapping.Where(c => c.VideoID == prodvideo.Id && c.UserID == UserID).Count() != 0)
                    //{
                    //    productvideo.Add(new ProductVideoModel
                    //    {
                    //        Id = prodvideo.Id,
                    //        Title = prodvideo.Title,
                    //        Decription = prodvideo.Decription,
                    //        VideoURL = prodvideo.VideoURL,
                    //        IsPaid = true,
                    //        ProductId = prodvideo.ProductId,
                    //        VideoRate = prodvideo.VideoRate,
                    //        SubcatId = prodvideo.SubcatId
                    //    });
                    //}
                    //else
                    //{
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
                    //}
                }
                return productvideo;
            }
        }

        public IEnumerable<CardTransactionDetailsMappings> GetBuyProductList()
        {
            using (var context = new UniversityEntities())
            {
                int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                
                var res =         (from ctd in context.CardTransactionDeatilsMapping.Where(c => c.UserID == UserID && c.IsDeleted==false)
                                   join v in context.ProductVideos
                                   on ctd.VideoID equals v.Id
                                   select new CardTransactionDetailsMappings
                                   {
                                      CTMID = ctd.CTMID,
                                      VideoID = ctd.VideoID,
                                      UserID = ctd.UserID,
                                      //IsPaid = ctd.IsPaid,
                                      CategoryID = ctd.CategoryID,
                                      TransactionID = ctd.TransactionID,
                                      ProductID = ctd.ProductID,
                                      VideoRate = v.VideoRate ??0
                                  }).ToList();

                return res;
            }
           
        }
    }



}
