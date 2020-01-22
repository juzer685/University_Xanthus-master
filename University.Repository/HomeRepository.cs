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
                int AssociatedUserID = Convert.ToInt32(HttpContext.Current.Session["UserSessionIDs"]);
                if (AssociatedUserID != 0)
                {

                    return context.HomeSlider.Include("Product").Where(y => y.IsDeleted != true && y.AssocitedID == AssociatedUserID).OrderByDescending(y => y.CreatedDate).ToList();
                }
                else if (AssociatedUserID == 0)
                {
                    //int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                    return context.HomeSlider.Include("Product").Where(y => y.IsDeleted != true ).OrderByDescending(y => y.CreatedDate).ToList();

                }
                else
                {
                    return context.HomeSlider.Include("Product").Where(y => y.IsDeleted != true).OrderByDescending(y => y.CreatedDate).ToList();
                }

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
                var slider = context.HomeSlider.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
                if (slider != null)
                {
                    slider.ProductId = model.ProductId;
                    slider.TextDescription = model.TextDescription;
                    slider.Link = model.Link;
                    slider.ImageALT = model.ImageALT;
                    if (!string.IsNullOrWhiteSpace(model.ImageURL))
                    {
                        slider.ImageURL = model.ImageURL;
                    }
                    slider.UpdatedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                else
                {
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
                int AssociatedUserID = Convert.ToInt32(HttpContext.Current.Session["UserSessionIDs"]);
                if (AssociatedUserID != 0)
                {

                    return context.HomeBanner.Where(y => y.IsDeleted != null && y.IsDeleted != true && y.AssocitedID == AssociatedUserID).OrderByDescending(y => y.UpdatedDate).FirstOrDefault();

                }
                else
                {
                    int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);

                    return context.HomeBanner.Where(y => y.IsDeleted != null && y.IsDeleted != true && y.AssocitedID == UserID).OrderByDescending(y => y.UpdatedDate).FirstOrDefault();
                }


            }
        }

        public bool AddOrUpdateHomeBanner(HomeBanner model)
        {
            using (var context = new UniversityEntities())
            {
                var banner = context.HomeBanner.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
                if (banner != null)
                {
                    banner.Description = model.Description;
                    banner.Title = model.Title;
                    banner.ImageALT = model.ImageALT;
                    banner.LinkTo = model.LinkTo;
                    banner.AssocitedID = model.AssocitedID;
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
                int AssociatedUserID = Convert.ToInt32(HttpContext.Current.Session["UserSessionIDs"]);
                if (AssociatedUserID != 0)
                {
                    var res = context.FAQ.Where(y => y.IsDeleted != true && y.AssocitedID == AssociatedUserID).OrderByDescending(y => y.CreatedDate).ToList();
                    return res.Select(y => new FAQ()
                    {
                        Id = y.Id,
                        Question = y.Question,
                        Answer = y.Answer,
                        AssocitedID = y.AssocitedID,
                        FAQDoc = null
                    }).OrderByDescending(y => y.CreatedDate).ToList();
                }
                else
                {
                    int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                    var res = context.FAQ.Where(y => y.IsDeleted != true && y.AssocitedID == UserID).OrderByDescending(y => y.CreatedDate).ToList();
                    return res.Select(y => new FAQ()
                    {
                        Id = y.Id,
                        Question = y.Question,
                        Answer = y.Answer,
                        AssocitedID = y.AssocitedID,
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
                var faq = context.FAQ.Include("FAQDoc").FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
                if (faq != null)
                {
                    faq.Question = model.Question;
                    faq.Answer = model.Answer;
                    faq.AssocitedID = model.AssocitedID;


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
            using (var context = new UniversityEntities())
            {
                int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                var res = (from p in context.Product.Where(y => y.IsDeleted != true)
                           join s in context.SubCategoryMaster.Where(y => y.IsDeleted != true)
                           on p.SubCategoryId equals s.Id
                           //join cm in context.CategoryUserMapping.Where(y=>y.IsDeleted !=true )
                           //on s.Id equals cm.CategoryID
                           //join l in context.Login_tbl.Where(y=>y.IsDeleted!=true)
                           //on cm.UserID equals l.ID 
                           join c in context.CategoryMaster.Where(y => y.IsDeleted != true)
                           on s.CategoryId equals c.Id
                           join g in context.ProductUserGuide.Where(y => y.IsDeleted != true)
                           on p.Id equals g.ProductId into temp
                           from guide in temp.DefaultIfEmpty()
                               //join v in context.ProductVideos.Where(y => y.IsDeleted != true)
                               //on p.Id equals v.ProductId into tempV
                               //from videos in tempV.DefaultIfEmpty()

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
                               CategoryMaster = c,
                               SubCategoryMaster = s,
                               ProductUserGuide = guide,
                               Categorymapp = context.CategoryUserMapping.Where(x=> x.IsDeleted != true && x.UserID == UserID).ToList(),
                               ProductVideos = context.ProductVideos.Where(x => x.IsDeleted != true).ToList(),
                               ProductDocuments = context.ProductDocuments.Where( x=> x.IsDeleted != true).ToList()
                           }).ToList();
                return res.OrderByDescending(x => x.CreatedDate).Take(6);
            }
        }
        #endregion
    }

}
