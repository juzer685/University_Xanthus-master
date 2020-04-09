using University.Data;
using University.Data.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repository.Interface
{
    public interface IProductRepository
    {
        CoursePreviewVideos GetCoursePrviewVideo(Decimal CourseID);
        bool SaveCoursePreviewVideo(CoursePreviewVideos coursePreviewVideos);
        IEnumerable<ProductVideos> GetUserVideosLists();
 
        IEnumerable<ProductVideos> GetUserVideosList();
        IEnumerable<ProductEntity> GetProductList();
        ProductEntity GetProduct(Decimal Id);
        //bool AddOrUpdateProduct(ProductEntity model);
        bool DeleteProduct(Decimal id);
        ProductEntity GetProductByCategory(Decimal Id);
        Decimal AddUpdateProductBasic(ProductEntity model);
        Decimal SaveProductFAQ(ProductFAQs productFAQ);
        ProductFAQs GetProductFAQ(Decimal productFAQId);
        List<ProductFAQs> GetProductFAQs(Decimal productId);
        bool DeleteProductFAQ(Decimal productFAQId);
        Decimal SaveProductFAQVideo(ProductFAQVideos productFAQVideo);
        bool DeleteProductFAQVideo(int productFAQVideoId);
        ProductFAQVideos GetProductFAQVideo(Decimal productFAQVideoId);
        ProductSpec GetProductSpecification(Decimal ProductId);
        bool SaveProductSpecification(ProductSpec productSpec);
        ProductUserGuide GetProductUserGuide(Decimal ProductId);
        bool SaveProductUserGuide(ProductUserGuide productUserGuide);
        Decimal SaveProductVideo(ProductVideos productVideo);
        bool DeleteProductVideo(Decimal productVideoId);
        ProductVideos GetProductVideo(Decimal productVideoId);
        Decimal SaveProductDocument(ProductDocuments productDocument);
        bool DeleteProductDocument(Decimal productDocumentId);
        ProductDocuments GetProductDocument(Decimal productDocumentId);
        List<ProductEntity> GetProducts();
        List<ProductFeedback> GetProductFeedback();
        List<ProductLayoutMenu> GetProductLayouMenu();
        bool UpdateProductvideo(ProductVideos model);
    }
}
