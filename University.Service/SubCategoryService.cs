using University.Data;
using University.Repository.Interface;
using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        public SubCategoryService(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;

        }
        public bool AddOrUpdateSubCategory(SubCategoryMaster model)
        {
            return _subCategoryRepository.AddOrUpdateSubCategory(model);
        }

        public bool DeleteSubCategory(Decimal id)
        {
            return _subCategoryRepository.DeleteSubCategory(id);
        }

        public SubCategoryMaster GetSubCategory(Decimal id)
        {
            return _subCategoryRepository.GetSubCategory(id);
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
