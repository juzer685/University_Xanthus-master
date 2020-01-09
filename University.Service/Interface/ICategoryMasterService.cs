using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service.Interface
{
    public interface ICategoryMasterService
    {
        IEnumerable<CategoryMaster> GetCategoryList();
        CategoryMaster GetCategory(Decimal id);
        bool AddOrUpdateCategory(CategoryMaster model);
        bool DeleteCategory(Decimal id);
        List<CategoryMaster> GetCategoryMasters();
        List<SmartSerach_Result> SmartSearch(string freeText);
    }
}
