using University.Repository;
using University.Repository.Interface;
using University.Service;
using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace University.UI
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        public IUnityContainer container = BuildUnityContainer();
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here  
            //This is the important line to edit  
            container.RegisterType<IUserTestRepository, UserTestRepository>();
            container.RegisterType<IUserTestService, UserTestService>();
            container.RegisterType<ICategoryMasterService, CategoryMasterService>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IFeedbackService, FeedbackService>();
            container.RegisterType<IFeedbackRepository, FeedbackRepository>();
            container.RegisterType<ISubCategoryRepository, SubCategoryRepository>();
            container.RegisterType<ISubCategoryService, SubCategoryService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductVideoRepository, ProductVideoRepository>();
            container.RegisterType<IProductVideoService, ProductVideoService>();
            container.RegisterType<IProductUserGuideService, ProductUserGuideService>();
            container.RegisterType<IProductUserGuideRepository, ProductUserGuideRepository>();
            container.RegisterType<IHomeService, HomeService>();
            container.RegisterType<IHomeRepository, HomeRepository>();
            container.RegisterType<IPaymentGatewayService, PaymentGatewayService>();
            container.RegisterType<IPaymentGatewayRepository, PaymentGatewayRepository>();
            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}