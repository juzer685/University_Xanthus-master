using AutoMapper;
using University.Data;
using University.Data.CustomEntities;
using University.UI.Areas.Admin.Models;
using University.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.App_Start
{
    public class MapperConfg : Profile
    {

        public MapperConfg()
        {
            CreateMap<CategoryMaster, CategoryViewModel>();
            CreateMap<CategoryViewModel, CategoryMaster>();
            CreateMap<SubCategoryMaster, SubCategoryViewModel>();
            CreateMap<SubCategoryViewModel, SubCategoryMaster>();
            CreateMap<ProductEntity, ProductViewModel>();
            CreateMap<ProductViewModel, ProductEntity>();
            CreateMap<ProductUserGuide, ProductUserGuideViewModel>();
            CreateMap<ProductUserGuideViewModel, ProductUserGuide>();
            CreateMap<SubCategoryMaster, DropDownViewModel>().ForMember(des => des.Value, src => src.MapFrom(y => y.Id));
            CreateMap<ProductVideos, ProductVideoViewModel>();
            CreateMap<ProductVideoViewModel, ProductVideos>();
            CreateMap<ProductSpec, ProductSpecViewModel>();
            CreateMap<ProductSpecViewModel, ProductSpec>();
            CreateMap<ProductFAQs, ProductFaqViewModel>();
            CreateMap<FAQ, FAQViewModel>();
            CreateMap<ProductFaqViewModel, ProductFAQs>();
            CreateMap<ProductFAQVideoViewModel, ProductFAQVideos>();
            CreateMap<ProductFAQVideos, ProductFAQVideoViewModel>();
            CreateMap<ProductDocumentViewModel, ProductDocuments>();
            CreateMap<ProductDocuments, ProductDocumentViewModel>();
            CreateMap<ProductFeedback, ProductFeedbackViewModel>();
            CreateMap<ProductFeedbackViewModel, ProductFeedback>();
            CreateMap<HomeSlider, HomeSliderViewModel>();
            CreateMap<HomeSliderViewModel, HomeSlider>();
            CreateMap<ProductEntity, ProductDropdownListViewModel>();
            CreateMap<ProductDropdownListViewModel, ProductEntity>();
        }
    }
}