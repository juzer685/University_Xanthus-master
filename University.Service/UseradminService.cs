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
    public class UseradminService: IUseradminService
    {
        private readonly IUseradminRepository _UseradminRepository;
        public UseradminService()
        {
            _UseradminRepository = new  UseradminRepository();
         
        }

        public IEnumerable<Login_tbl> GetUserList()
        {
            return _UseradminRepository.GetUserList();
        }

        //public CategoryMaster GetCategory(Decimal id)
        //{
        //    return _categoryRepository.GetCategory(id);
        //}

        //public bool AddOrUpdateCategory(CategoryMaster model)
        //{
        //    return _categoryRepository.AddOrUpdateCategory(model);
        //}
        public bool DeleteUser(Decimal id)
        {
            return _UseradminRepository.DeleteUser(id);
        }
        public (bool, bool) RegisterUser(Login_tbl Login_tbl)
        {
            return _UseradminRepository.RegisterUser(Login_tbl);
        }

        public List<Customer> GetCustomerList()
        {
            return _UseradminRepository.GetCustomerList();
        }

        public Login_tbl EditUser(int id)
        {
            return _UseradminRepository.EditUser(id);
        }

        //public List<CategoryMaster> GetCategoryMasters()
        //{
        //    return _categoryRepository.GetCategoryMasters();
        //}
        //public List<SmartSerach_Result> SmartSearch(string freeText)
        //{
        //    return _categoryRepository.SmartSearch(freeText);
        //}
    }
}
