using University.Data;
using University.Repository.Interface;
using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core;

namespace University.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        public IEnumerable<SubCategoryMaster> GetCategoryUserList(decimal test)
        {
            return _subCategoryRepository.GetCategoryUserList(test);
        }

        public List<CategoryModel> BindCategories(decimal UserID)
        {
            return _subCategoryRepository.BindCategories(UserID);
        }

        public bool DeleteCategoryUseerMapping (Decimal id)
        {
            return _subCategoryRepository.DeleteCategoryUseerMapping(id);
        }
        public SubCategoryService(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;

        }
        public bool AddCategoryUserMapping(CategoryUserMapping model)
        {
            return _subCategoryRepository.AddCategoryUserMapping(model);
        }
        public bool AddOrUpdateSubCategory(SubCategoryMaster model)
        {
            return _subCategoryRepository.AddOrUpdateSubCategory(model);
        }

        public bool DeleteSubCategory(Decimal id)
        {
            return _subCategoryRepository.DeleteSubCategory(id);
        }

        public CategoryUserMapping GetCategoryUserMapping(Decimal id)
        {
            return _subCategoryRepository.GetCategoryUserMapping(id);
        }

        public SubCategoryMaster GetSubCategory(Decimal id)
        {
            return _subCategoryRepository.GetSubCategory(id);
        }
        //public List<SubCategoryMaster> GetCategoryUservideoMappingList()
        //{
        //    return _subCategoryRepository.GetCategoryUservideoMappingList();
        //}
        public (List<Login_tbl>,List<SubCategoryMaster>)GetCategoryUserMappingList()
        {
            return _subCategoryRepository.GetCategoryUserMappingList();
        }
        public List<CategoryUserMapping> GetCategoryUserMappingGrid()
        {
            return _subCategoryRepository.GetCategoryUserMappingGrid();
        }

        public IEnumerable<SubCategoryMaster> GetSubCategoryList()
        {
            return _subCategoryRepository.GetSubCategoryList();
        }
        public List<SubCategoryMaster> GetSubCategoryListById(Decimal id)
        {
            return _subCategoryRepository.GetSubCategoryListById(id);
        }
        public IEnumerable<SubCategoryMaster> GetSubCategoryList(Decimal CategoryId)
        {
            return _subCategoryRepository.GetSubCategoryList(CategoryId);
        }
        public IEnumerable<SubCategoryMaster> GetSubCategoryListOnlyHaveProduct()
        {
            return _subCategoryRepository.GetSubCategoryListOnlyHaveProduct();
        }
    }
}
