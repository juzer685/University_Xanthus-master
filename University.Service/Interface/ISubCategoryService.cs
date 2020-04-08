using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core;

namespace University.Service.Interface
{
    public interface ISubCategoryService
    {
        IEnumerable<SubCategoryMaster> GetCategoryUserList(decimal test);
        List<CategoryModel> BindCategories(decimal UserID);
        bool DeleteCategoryUseerMapping(Decimal id);
        List<CategoryUserMapping> GetCategoryUserMappingGrid();
        (List<Login_tbl>,List<SubCategoryMaster>) GetCategoryUserMappingList();
        IEnumerable<SubCategoryMaster> GetSubCategoryList();
        List<SubCategoryMaster> GetSubCategoryListById(Decimal id);
        CategoryUserMapping GetCategoryUserMapping(Decimal id);
        SubCategoryMaster GetSubCategory(Decimal id);
        bool AddOrUpdateSubCategory(SubCategoryMaster model);
        bool AddCategoryUserMapping(CategoryUserMapping model);
        bool DeleteSubCategory(Decimal id);
        IEnumerable<SubCategoryMaster> GetSubCategoryList(Decimal CategoryId);
        IEnumerable<SubCategoryMaster> GetSubCategoryListOnlyHaveProduct();
    }
}
