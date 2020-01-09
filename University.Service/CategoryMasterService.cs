using University.Data;
using University.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Service.Interface;
using University.Repository.Interface;

namespace University.Service
{
    public class CategoryMasterService:ICategoryMasterService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryMasterService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
         
        }

        public IEnumerable<CategoryMaster> GetCategoryList()
        {
            return _categoryRepository.GetCategoryList();
        }

        public CategoryMaster GetCategory(Decimal id)
        {
            return _categoryRepository.GetCategory(id);
        }

        public bool AddOrUpdateCategory(CategoryMaster model)
        {
            return _categoryRepository.AddOrUpdateCategory(model);
        }
        public bool DeleteCategory(Decimal id)
        {
            return _categoryRepository.DeleteCategory(id);
        }
        public List<CategoryMaster> GetCategoryMasters()
        {
            return _categoryRepository.GetCategoryMasters();
        }
        public List<SmartSerach_Result> SmartSearch(string freeText)
        {
            return _categoryRepository.SmartSearch(freeText);
        }
    }
}
