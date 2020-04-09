using University.Data;
using University.Data.CustomEntities;
using University.Repository.Interface;
using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace University.Service
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public IEnumerable<ProductVideos> GetUserVideosLists()
        {
            return _productRepository.GetUserVideosLists();
        }
        public IEnumerable<ProductVideos> GetUserVideosList()
        {
            return _productRepository.GetUserVideosList();
        }
       

        public IEnumerable<ProductEntity> GetProductList()
        {
           return _productRepository.GetProductList();
        }

        public ProductEntity GetProduct(Decimal Id)
        {
            return _productRepository.GetProduct(Id);
        }
        public bool AddOrUpdateProduct(ProductEntity model)
        {
            return true;//_productRepository.AddOrUpdateProduct(model);
        }
        public bool DeleteProduct(Decimal id)
        {
            return _productRepository.DeleteProduct(id);
        }

        public ProductEntity GetProductByCategory(Decimal Id)
        {
            return _productRepository.GetProductByCategory(Id);
        }
        public Decimal AddUpdateProductBasic(ProductEntity model)
        {
            return _productRepository.AddUpdateProductBasic(model);
        }
        public Decimal SaveProductFAQ(ProductFAQs productFAQ)
        {
            return _productRepository.SaveProductFAQ(productFAQ);
        }
        public bool DeleteProductFAQ(Decimal productFAQId)
        {
            return _productRepository.DeleteProductFAQ(productFAQId);
        }
        public ProductFAQs GetProductFAQ(Decimal productFAQId)
        {
            return _productRepository.GetProductFAQ(productFAQId);
        }
        public List<ProductFAQs> GetProductFAQs(Decimal productId)
        {
            return _productRepository.GetProductFAQs(productId);
        }

        public Decimal SaveProductFAQVideo(ProductFAQVideos productFAQVideo)
        {
            return _productRepository.SaveProductFAQVideo(productFAQVideo);
        }
        public bool DeleteProductFAQVideo(int productFAQVideoId)
        {
            return _productRepository.DeleteProductFAQVideo(productFAQVideoId);
        }
        public ProductFAQVideos GetProductFAQVideo(Decimal productFAQVideoId)
        {
            return _productRepository.GetProductFAQVideo(productFAQVideoId);
        }
        public ProductSpec GetProductSpecification(Decimal ProductId)
        {
            return _productRepository.GetProductSpecification(ProductId);
        }
        public bool SaveProductSpecification(ProductSpec productSpec)
        {
            return _productRepository.SaveProductSpecification(productSpec);
        }
        public ProductUserGuide GetProductUserGuide(Decimal ProductId)
        {
            return _productRepository.GetProductUserGuide(ProductId); ;
        }
        public bool SaveProductUserGuide(ProductUserGuide productUserGuide)
        {
            return _productRepository.SaveProductUserGuide(productUserGuide);
        }
        public bool SaveCoursePreviewVideo(CoursePreviewVideos coursePreviewVideos)
        {
            return _productRepository.SaveCoursePreviewVideo(coursePreviewVideos);
        }
        public Decimal SaveProductVideo(ProductVideos productVideo)
        {
            return _productRepository.SaveProductVideo(productVideo);
        }
        //public Decimal SaveProductVideoRate(ProductVideos productVideo)
        //{
        //    return _productRepository.SaveProductVideo(productVideo);
        //}
        public bool DeleteProductVideo(Decimal productVideoId)
        {
            return _productRepository.DeleteProductVideo(productVideoId);
        }
        public ProductVideos GetProductVideo(Decimal productVideoId)
        {
            return _productRepository.GetProductVideo(productVideoId);
        }
        public CoursePreviewVideos GetCoursePrviewVideo(Decimal CourseID)
        {
            return _productRepository.GetCoursePrviewVideo(CourseID);
        }
        public Decimal SaveProductDocument(ProductDocuments productDocument)
        {
            return _productRepository.SaveProductDocument(productDocument);
        }
        public bool DeleteProductDocument(Decimal productDocumentId)
        {
            return _productRepository.DeleteProductDocument(productDocumentId);
        }
        public ProductDocuments GetProductDocument(Decimal productDocumentId)
        {
            return _productRepository.GetProductDocument(productDocumentId);
        }
        public List<ProductEntity> GetProducts()
        {
            return _productRepository.GetProducts();
        }
        public List<ProductFeedback> GetProductFeedback()
        {
            return _productRepository.GetProductFeedback();
        }
        public List<ProductLayoutMenu> GetProductLayouMenu()
        {
            return _productRepository.GetProductLayouMenu();
        }
        
        public bool UpdateProductvideo(ProductVideos model)
        {
            return _productRepository.UpdateProductvideo(model);
        }

        public object GetUserVideosLists(string productVideoId)
        {
            throw new NotImplementedException();
        }
    }
}
