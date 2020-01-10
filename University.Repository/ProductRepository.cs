using University.Data;
using University.Data.CustomEntities;
using University.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace University.Repository
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<ProductEntity> GetProductList()
        {
            using (var context = new UniversityEntities())
            {
                int AssociatedUserID = Convert.ToInt32(HttpContext.Current.Session["UserSessionIDs"]);
                var res = (from p in context.Product.Where(y => y.IsDeleted != true && y.AssocitedID == AssociatedUserID)
                           join s in context.SubCategoryMaster.Where(y => y.IsDeleted != true)
                           on p.SubCategoryId equals s.Id
                           join c in context.CategoryMaster.Where(y => y.IsDeleted != true)
                           on s.CategoryId equals c.Id
                           select new ProductEntity()
                           {
                               AssocitedID=p.AssocitedID,
                               Id = p.Id,
                               //CategoryId = c.Id,
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
                               SubCategoryMaster = s
                           }).OrderByDescending(y => y.CreatedDate).ToList();
                return res;
            }
        }

        public ProductEntity GetProduct(Decimal Id)
        {
            using (var context = new UniversityEntities())
            {
                var res = (from p in context.Product.Where(y => y.IsDeleted != true)
                           join s in context.SubCategoryMaster.Where(y => y.IsDeleted != true)
                           on p.SubCategoryId equals s.Id
                           join c in context.CategoryMaster.Where(y => y.IsDeleted != true)
                           on s.CategoryId equals c.Id
                           join g in context.ProductUserGuide.Where(y => y.IsDeleted != true)
                           on p.Id equals g.ProductId into temp
                           from guide in temp.DefaultIfEmpty()
                               //join v in context.ProductVideos.Where(y => y.IsDeleted != true)
                               //on p.Id equals v.ProductId into tempV
                               //from videos in tempV.DefaultIfEmpty()
                           join spec in context.ProductSpec.Where(y => y.IsDeleted != true)
                           on p.Id equals spec.ProductId into tempS
                           from speci in tempS.DefaultIfEmpty()
                           where p.Id == Id
                           select new ProductEntity()
                           {
                               Id = p.Id,
                               //CategoryId = c.Id,
                               CreatedBy = p.CreatedBy,
                               CreatedDate = p.CreatedDate,
                               DeletedBy = p.DeletedBy,
                               DeletedDate = p.DeletedDate,
                               Description = p.Description,
                               ImageURL = p.ImageURL,
                               ImageALT = p.ImageALT,
                               IsDeleted = p.IsDeleted,
                               SubCategoryId = p.SubCategoryId,
                               Title = p.Title,
                               UpdatedBy = p.UpdatedBy,
                               UpdatedDate = p.UpdatedDate,
                               CategoryMaster = c,
                               SubCategoryMaster = s,
                               ProductUserGuide = guide,
                               ProductVideos = p.ProductVideos.Where(y => y.IsDeleted != true).ToList(),
                               ProductSpec = speci,
                               ProductFAQs = p.ProductFAQs.Where(y => y.IsDeleted != true).ToList(),
                               ProductDocuments = p.ProductDocuments.Where(y => y.IsDeleted != true).ToList()
                           }).ToList();
                return res.FirstOrDefault();
            }
        }

        //public bool AddOrUpdateProduct(ProductEntity model)
        //{
        //    using (var context = new IPSU_devEntities())
        //    {
        //        var product = context.Products.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
        //        if (product != null)
        //        {
        //            if (!string.IsNullOrWhiteSpace(model.ImageURL))
        //            {
        //                product.ImageURL = model.ImageURL;
        //            }
        //            product.Title = model.Title;
        //            product.SubCategoryId = model.SubCategoryId;
        //            product.UpdatedDate = DateTime.UtcNow;
        //            var userGuide = context.ProductUserGuides.FirstOrDefault(y => y.ProductId == model.Id && y.IsDeleted != true);
        //            if (model.ProductUserGuide != null)
        //            {
        //                if (userGuide != null)
        //                {
        //                    if (!string.IsNullOrWhiteSpace(model.ProductUserGuide.Title))
        //                        userGuide.Title = model.ProductUserGuide.Title;
        //                    if (!string.IsNullOrWhiteSpace(model.ProductUserGuide.Description))
        //                        userGuide.Description = model.ProductUserGuide.Description;
        //                    if (!string.IsNullOrWhiteSpace(model.ProductUserGuide.ImageURL))
        //                        userGuide.ImageURL = model.ProductUserGuide.ImageURL;
        //                    if (!string.IsNullOrWhiteSpace(model.ProductUserGuide.ImageURL) || !string.IsNullOrWhiteSpace(model.ProductUserGuide.Title))
        //                        userGuide.UpdatedDate = DateTime.UtcNow;
        //                }
        //                else
        //                {
        //                    model.ProductUserGuide.Id = Guid.NewGuid();
        //                    product.ProductUserGuides.Add(model.ProductUserGuide);
        //                }
        //            }

        //            var video = context.ProductVideos.FirstOrDefault(y => y.ProductId == model.Id && y.IsDeleted != true);
        //            if (model.ProductVideos != null)
        //            {
        //                if (video != null)
        //                {
        //                    if (!string.IsNullOrWhiteSpace(model.ProductVideos.Title))
        //                        video.Title = model.ProductVideos.Title;
        //                    if (!string.IsNullOrWhiteSpace(model.ProductVideos.Decription))
        //                        video.Title = model.ProductVideos.Decription;
        //                    if (!string.IsNullOrWhiteSpace(model.ProductVideos.VideoURL))
        //                        video.VideoURL = model.ProductVideos.VideoURL;
        //                    if (!string.IsNullOrWhiteSpace(model.ProductVideos.VideoURL) || !string.IsNullOrWhiteSpace(model.ProductUserGuide.Title))
        //                        video.UpdatedDate = DateTime.UtcNow;
        //                }
        //                else
        //                {
        //                    model.ProductVideos.Id = Guid.NewGuid();
        //                    product.ProductVideos.Add(model.ProductVideos);
        //                }
        //            }

        //            var speci = context.ProductSpecs.FirstOrDefault(y => y.ProductId == model.Id && y.IsDeleted != true);
        //            if (model.ProductSpec != null)
        //            {
        //                if (speci != null)
        //                {
        //                    if (!string.IsNullOrWhiteSpace(model.ProductSpec.Title))
        //                        speci.Title = model.ProductSpec.Title;
        //                    if (!string.IsNullOrWhiteSpace(model.ProductSpec.Description))
        //                        speci.Description = model.ProductSpec.Description;
        //                    if (!string.IsNullOrWhiteSpace(model.ProductSpec.Description) || !string.IsNullOrWhiteSpace(model.ProductSpec.Title))
        //                        speci.UpdatedDate = DateTime.UtcNow;
        //                }
        //                else
        //                {
        //                    model.ProductSpec.Id = Guid.NewGuid();
        //                    product.ProductSpecs.Add(model.ProductSpec);
        //                }
        //            }

        //            var faqs = context.ProductFAQs.Where(y => y.ProductId == model.Id && y.IsDeleted != true);
        //            if (faqs.Count() > 0)
        //            {
        //                context.ProductFAQs.RemoveRange(faqs);
        //            }
        //            if (model.ProductFAQs != null && model.ProductFAQs.Count > 0)
        //            {
        //                foreach (var item in model.ProductFAQs)
        //                {
        //                    if (item.Id == Guid.Empty)
        //                        item.Id = Guid.NewGuid();
        //                    item.ProductId = model.Id;
        //                    product.ProductFAQs.Add(item);
        //                }
        //            }

        //            context.SaveChanges();
        //        }
        //        else
        //        {
        //            var Productobj = new Product();
        //            Productobj.Id = Guid.NewGuid();
        //            Productobj.CreatedDate = DateTime.UtcNow;
        //            Productobj.Title = model.Title;
        //            Productobj.Description = model.Description;
        //            Productobj.ImageURL = model.ImageURL;
        //            Productobj.SubCategoryId = model.SubCategoryId;
        //            if (model.ProductUserGuide != null)
        //            {
        //                model.ProductUserGuide.Id = Guid.NewGuid();
        //                Productobj.ProductUserGuides.Add(model.ProductUserGuide);
        //            }
        //            if (model.ProductVideos != null)
        //            {
        //                model.ProductVideos.Id = Guid.NewGuid();
        //                Productobj.ProductVideos.Add(model.ProductVideos);
        //            }
        //            if (model.ProductSpec != null)
        //            {
        //                model.ProductSpec.Id = Guid.NewGuid();
        //                Productobj.ProductSpecs.Add(model.ProductSpec);
        //            }
        //            if (model.ProductFAQs != null && model.ProductFAQs.Count > 0)
        //            {
        //                foreach (var item in model.ProductFAQs)
        //                {
        //                    if (item.Id == Guid.Empty)
        //                        item.Id = Guid.NewGuid();
        //                    Productobj.ProductFAQs.Add(item);
        //                }
        //            }
        //            context.Products.Add(Productobj);
        //            context.SaveChanges();
        //        }
        //        return true;
        //    }
        //}

        public Decimal AddUpdateProductBasic(ProductEntity model)
        {
            using (var context = new UniversityEntities())
            {
                var product = context.Product.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
                if (product != null)
                {
                    if (!string.IsNullOrWhiteSpace(model.ImageURL))
                    {
                        product.ImageURL = model.ImageURL;
                    }
                    product.Title = model.Title;
                    product.SubCategoryId = model.SubCategoryId;
                    product.Description = model.Description;
                    product.UpdatedDate = DateTime.UtcNow;
                    product.ImageALT = model.ImageALT;
                    product.AssocitedID = model.AssocitedID;
                    context.SaveChanges();
                    return product.Id;
                }
                else
                {
                    var Productobj = new Product();
                    Productobj.CreatedDate = DateTime.UtcNow;
                    Productobj.Title = model.Title;
                    Productobj.Description = model.Description;
                    Productobj.ImageURL = model.ImageURL;
                    Productobj.SubCategoryId = model.SubCategoryId;
                    Productobj.ImageALT = model.ImageALT;
                    Productobj.AssocitedID = model.AssocitedID;
                    context.Product.Add(Productobj);
                    context.SaveChanges();
                    return Productobj.Id;
                }
            }
        }
        public bool DeleteProduct(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                var product = context.Product.FirstOrDefault(y => y.Id == id && y.IsDeleted != true);
                if (product != null)
                {
                    product.IsDeleted = true;
                    product.DeletedDate = DateTime.UtcNow;
                    if (product.ProductUserGuide.ToList().Count() > 0)
                    {
                        product.ProductUserGuide.FirstOrDefault().IsDeleted = true;
                    }
                    context.SaveChanges();
                }
                return true;
            }
        }

        public ProductEntity GetProductByCategory(Decimal Id)
        {
            using (var context = new UniversityEntities())
            {
                var res = (from p in context.Product.Where(y => y.IsDeleted != true)
                           join s in context.SubCategoryMaster.Where(y => y.IsDeleted != true)
                           on p.SubCategoryId equals s.Id
                           join c in context.CategoryMaster.Where(y => y.IsDeleted != true)
                           on s.CategoryId equals c.Id
                           join g in context.ProductUserGuide.Where(y => y.IsDeleted != true)
                           on p.Id equals g.ProductId into temp
                           from guide in temp.DefaultIfEmpty()
                               //join v in context.ProductVideos.Where(y => y.IsDeleted != true)
                               //on p.Id equals v.ProductId into tempV
                               //from videos in tempV.DefaultIfEmpty()
                           where p.SubCategoryId == Id
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
                               ProductVideos = context.ProductVideos.Where(y => y.IsDeleted != true).ToList(),
                               ProductDocuments = context.ProductDocuments.Where(y => y.IsDeleted != true).ToList()
                           }).ToList();
                return res.FirstOrDefault();
            }
        }

        public Decimal SaveProductFAQ(ProductFAQs productFAQ)
        {
            using (var context = new UniversityEntities())
            {
                var productFaq = context.ProductFAQs.FirstOrDefault(y => y.Id == productFAQ.Id && y.IsDeleted != true);
                if (productFaq != null)
                {
                    productFaq.UpdatedDate = DateTime.UtcNow;
                    productFaq.Question = productFAQ.Question;
                    productFaq.Answer = productFAQ.Answer;
                    context.SaveChanges();
                    return productFaq.Id;
                }
                else
                {
                    productFAQ.CreatedDate = DateTime.UtcNow;
                    context.ProductFAQs.Add(productFAQ);
                    context.SaveChanges();
                    return productFAQ.Id;
                }

            }
        }
        public bool DeleteProductFAQ(Decimal productFAQId)
        {
            using (var context = new UniversityEntities())
            {
                var productFaq = context.ProductFAQs.FirstOrDefault(y => y.Id == productFAQId);
                if (productFaq != null)
                {
                    productFaq.IsDeleted = true;
                    productFaq.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
            }
            return true;
        }

        public ProductFAQs GetProductFAQ(Decimal productFAQId)
        {
            using (var context = new UniversityEntities())
            {
                var productFaq = context.ProductFAQs.Include("ProductFAQVideos").FirstOrDefault(y => y.Id == productFAQId && y.IsDeleted != true);
                return productFaq;
            }
        }
        public List<ProductFAQs> GetProductFAQs(Decimal productId)
        {
            using (var context = new UniversityEntities())
            {
                var productFaq = context.ProductFAQs.Include("ProductFAQVideos").Where(y => y.ProductId == productId && y.IsDeleted != true).ToList();
                return productFaq;
            }
        }


        public Decimal SaveProductFAQVideo(ProductFAQVideos productFAQVideo)
        {
            using (var context = new UniversityEntities())
            {
                var productFaqVideo = context.ProductFAQVideos.FirstOrDefault(y => y.Id == productFAQVideo.Id && y.IsDeleted != true);
                if (productFaqVideo != null)
                {
                    productFaqVideo.UpdatedDate = DateTime.UtcNow;
                    if (!string.IsNullOrWhiteSpace(productFAQVideo.VideoURL))
                    {
                        productFaqVideo.VideoURL = productFAQVideo.VideoURL;
                    }
                    if (!string.IsNullOrWhiteSpace(productFAQVideo.ImageURL))
                    {
                        productFaqVideo.ImageURL = productFAQVideo.ImageURL;
                    }
                    context.SaveChanges();
                    return productFaqVideo.Id;
                }
                else
                {
                    //productFAQ.Id = Guid.NewGuid();
                    productFAQVideo.CreatedDate = DateTime.UtcNow;
                    context.ProductFAQVideos.Add(productFAQVideo);
                    context.SaveChanges();
                    return productFAQVideo.Id;
                }

            }
        }
        public bool DeleteProductFAQVideo(int productFAQVideoId)
        {
            using (var context = new UniversityEntities())
            {
                var productFaqVideo = context.ProductFAQVideos.FirstOrDefault(y => y.Id == productFAQVideoId);
                if (productFaqVideo != null)
                {
                    productFaqVideo.IsDeleted = true;
                    productFaqVideo.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
            }
            return true;
        }
        public ProductFAQVideos GetProductFAQVideo(Decimal productFAQVideoId)
        {
            using (var context = new UniversityEntities())
            {
                var productFaqVideo = context.ProductFAQVideos.FirstOrDefault(y => y.Id == productFAQVideoId && y.IsDeleted != true);
                return productFaqVideo;
            }
        }

        public List<ProductEntity> GetProducts()
        {
            using (var context = new UniversityEntities())
            {
                var res = (from p in context.Product.Where(y => y.IsDeleted != true)
                           join s in context.SubCategoryMaster.Where(y => y.IsDeleted != true)
                           on p.SubCategoryId equals s.Id
                           join c in context.CategoryMaster.Where(y => y.IsDeleted != true)
                           on s.CategoryId equals c.Id
                           join g in context.ProductUserGuide.Where(y => y.IsDeleted != true && y.Title != null)
                           on p.Id equals g.ProductId into temp
                           from guide in temp.DefaultIfEmpty()
                           join v in context.ProductVideos.Where(y => y.IsDeleted != true && y.Title != null)
                           on p.Id equals v.ProductId into tempV
                           from videos in tempV.DefaultIfEmpty()
                           join spec in context.ProductSpec.Where(y => y.IsDeleted != true && y.Title != null)
                           on p.Id equals spec.ProductId into tempS
                           from speci in tempS.DefaultIfEmpty()
                           select new ProductEntity()
                           {
                               Id = p.Id,
                               //CategoryId = c.Id,
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
                               /*roductVideos= videos,*/
                               ProductSpec = speci,
                               ProductFAQs = p.ProductFAQs
                           }).ToList();
                return res;
            }
        }

        #region Specification
        public ProductSpec GetProductSpecification(Decimal ProductId)
        {
            using (var context = new UniversityEntities())
            {
                var spec = context.ProductSpec.FirstOrDefault(y => y.ProductId == ProductId && y.IsDeleted != true);
                return spec;
            }
        }
        public bool SaveProductSpecification(ProductSpec productSpec)
        {
            using (var context = new UniversityEntities())
            {
                var spec = context.ProductSpec.FirstOrDefault(y => y.Id == productSpec.Id && y.IsDeleted != true);
                if (spec != null)
                {
                    spec.Description = productSpec.Description;
                    spec.Title = productSpec.Title;
                    spec.AssocitedID = productSpec.AssocitedID;
                    spec.UpdatedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                else
                {
                    productSpec.CreatedDate = DateTime.UtcNow;

                    context.ProductSpec.Add(productSpec);
                    context.SaveChanges();
                }
                return true;
            }
        }
        #endregion

        #region User Guide
        public ProductUserGuide GetProductUserGuide(Decimal ProductId)
        {
            using (var context = new UniversityEntities())
            {
                var guide = context.ProductUserGuide.FirstOrDefault(y => y.ProductId == ProductId && y.IsDeleted != true);
                return guide;
            }
        }
        public bool SaveProductUserGuide(ProductUserGuide productUserGuide)
        {
            using (var context = new UniversityEntities())
            {
                var guide = context.ProductUserGuide.FirstOrDefault(y => y.Id == productUserGuide.Id && y.IsDeleted != true);
                if (guide != null)
                {
                    guide.Description = productUserGuide.Description;
                    guide.Title = productUserGuide.Title;
                    guide.DocumentURL = productUserGuide.DocumentURL;
                    if (productUserGuide.ImageURL != null)
                        guide.ImageURL = productUserGuide.ImageURL;
                    guide.UpdatedDate = DateTime.UtcNow;
                    guide.ImageALT = productUserGuide.ImageALT;
                    context.SaveChanges();
                }
                else
                {
                    productUserGuide.CreatedDate = DateTime.UtcNow;
                    context.ProductUserGuide.Add(productUserGuide);
                    context.SaveChanges();
                }
                return true;
            }
        }
        #endregion
        #region Product Videos
        public bool UpdateProductvideo(ProductVideos model)
        {
            using (var context = new UniversityEntities())
            {
                var productvideo = context.ProductVideos.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
                if (productvideo != null)
                {
                    productvideo.Decription = model.Decription;
                    productvideo.Title = model.Title;
                    productvideo.ProductId = model.ProductId;
                    productvideo.ThumbnailURL = model.ThumbnailURL;
                    productvideo.AssocitedID = model.AssocitedID;
                    if (!string.IsNullOrWhiteSpace(model.VideoURL))
                    {
                        productvideo.VideoURL = model.VideoURL;
                    }
                    productvideo.IsDeleted = false;
                    productvideo.UpdatedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                else
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.IsDeleted = false;
                    context.ProductVideos.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
        }



        public Decimal SaveProductVideo(ProductVideos productVideo)
        {
            using (var context = new UniversityEntities())
            {
                var productVideoExising = context.ProductVideos.FirstOrDefault(y => y.Id == productVideo.Id && y.IsDeleted != true);
                if (productVideoExising != null)
                {
                    productVideoExising.UpdatedDate = DateTime.UtcNow;
                    productVideoExising.Title = productVideo.Title;
                    productVideoExising.Decription = productVideo.Decription;
                    if (!string.IsNullOrWhiteSpace(productVideo.VideoURL))
                    {
                        productVideoExising.VideoURL = productVideo.VideoURL;
                    }
                    if (!string.IsNullOrWhiteSpace(productVideo.ThumbnailURL))
                    {
                        productVideoExising.ThumbnailURL = productVideo.ThumbnailURL;
                    }
                    context.SaveChanges();
                    return productVideoExising.Id;
                }
                else
                {
                    productVideo.CreatedDate = DateTime.UtcNow;
                    context.ProductVideos.Add(productVideo);
                    context.SaveChanges();
                    return productVideo.Id;
                }

            }
        }
        public bool DeleteProductVideo(Decimal productVideoId)
        {
            using (var context = new UniversityEntities())
            {
                var productVideo = context.ProductVideos.FirstOrDefault(y => y.Id == productVideoId);
                if (productVideo != null)
                {
                    productVideo.IsDeleted = true;
                    productVideo.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
            }
            return true;
        }
        public ProductVideos GetProductVideo(Decimal productVideoId)
        {
            using (var context = new UniversityEntities())
            {
                var productVideo = context.ProductVideos.FirstOrDefault(y => y.Id == productVideoId && y.IsDeleted != true);
                return productVideo;
            }
        }
        #endregion

        #region Product Documents
        public Decimal SaveProductDocument(ProductDocuments productDocument)
        {
            using (var context = new UniversityEntities())
            {
                var productDocumentExising = context.ProductDocuments.FirstOrDefault(y => y.Id == productDocument.Id && y.IsDeleted != true);
                if (productDocumentExising != null)
                {
                    productDocumentExising.UpdatedDate = DateTime.UtcNow;
                    if (!string.IsNullOrWhiteSpace(productDocument.DocumentURL))
                    {
                        productDocumentExising.DocumentURL = productDocument.DocumentURL;
                    }
                    productDocumentExising.Decription = productDocument.Decription;
                    productDocumentExising.DocumentDisplayName = productDocument.DocumentDisplayName;
                    productDocumentExising.Title = productDocument.Title;
                    context.SaveChanges();
                    return productDocumentExising.Id;
                }
                else
                {
                    productDocument.CreatedDate = DateTime.UtcNow;
                    context.ProductDocuments.Add(productDocument);
                    context.SaveChanges();
                    return productDocument.Id;
                }

            }
        }
        public bool DeleteProductDocument(Decimal productDocumentId)
        {
            using (var context = new UniversityEntities())
            {
                var productDocument = context.ProductDocuments.FirstOrDefault(y => y.Id == productDocumentId);
                if (productDocument != null)
                {
                    productDocument.IsDeleted = true;
                    productDocument.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
            }
            return true;
        }
        public ProductDocuments GetProductDocument(Decimal productDocumetId)
        {
            using (var context = new UniversityEntities())
            {
                var productDocument = context.ProductDocuments.FirstOrDefault(y => y.Id == productDocumetId && y.IsDeleted != true);
                return productDocument;
            }
        }
        #endregion

        #region Product Feedback
        public List<ProductFeedback> GetProductFeedback()
        {
            using (var context = new UniversityEntities())
            {
                var productFeedback = context.ProductFeedback.Include("Product").Where(y => y.IsDeleted != true).ToList();
                return productFeedback;
            }
        }


        #endregion

        #region Layout menu
        public List<ProductLayoutMenu> GetProductLayouMenu()
        {
            using (var context = new UniversityEntities())
            {
                var res = (from p in context.Product.Where(y => y.IsDeleted != true)
                           join s in context.SubCategoryMaster.Where(y => y.IsDeleted != true)
                           on p.SubCategoryId equals s.Id

                           select new ProductLayoutMenu()
                           {
                               ProductId = p.Id,
                               ProductName = p.Title,
                               SubCategoryId = s.Id,
                               SubCategoryName = s.Name
                           }).ToList();

                return res;
            }
        }
        #endregion
    }
}
