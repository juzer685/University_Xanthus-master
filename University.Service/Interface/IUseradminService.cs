using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service.Interface
{
    public interface IUseradminService 
    {
        IEnumerable<Login_tbl> GetUserList ();
        //CategoryMaster GetCategory(Decimal id);
        //bool AddOrUpdateCategory(CategoryMaster model);
        bool DeleteUser(Decimal id);
        //List<CategoryMaster> GetCategoryMasters();
        //List<SmartSerach_Result> SmartSearch(string freeText);
        bool RegisterUser(Login_tbl Login_tbl);
        List<Customer> GetCustomerList();
    }
}
