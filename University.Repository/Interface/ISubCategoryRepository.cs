using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repository.Interface
{
    public interface ISubCategoryRepository
    {
        List<CategoryUserMapping> GetCategoryUserMappingGrid();
        (List<Login_tbl>, List<SubCategoryMaster>) GetCategoryUserMappingList();
        bool AddCategoryUserMapping(CategoryUserMapping model);
        IEnumerable<SubCategoryMaster> GetSubCategoryList();
        List<SubCategoryMaster> GetSubCategoryListById(Decimal id);
        CategoryUserMapping GetCategoryUserMapping(Decimal id);
        SubCategoryMaster GetSubCategory(Decimal id);
        bool AddOrUpdateSubCategory(SubCategoryMaster model);
        bool DeleteSubCategory(Decimal id);
        IEnumerable<SubCategoryMaster> GetSubCategoryList(Decimal CategoryId);
        IEnumerable<SubCategoryMaster> GetSubCategoryListOnlyHaveProduct();
    }
}
