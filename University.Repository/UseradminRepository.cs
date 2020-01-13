using University.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Repository.Interface;

namespace University.Repository
{
    public class UseradminRepository: IUseradminRepository
    {

        public IEnumerable<Login_tbl> GetUserList()
        {
            using (var context = new UniversityEntities())
            {
                return context.Login_tbl.Where(y => y.IsDeleted == false).ToList();
            }
        }
        //    public CategoryMaster GetCategory(Decimal id)
        //    {
        //        using (var context = new UniversityEntities())
        //        {
        //            return context.CategoryMaster.FirstOrDefault(y=>y.Id== id && y.IsDeleted != true);
        //        }
        //    }

        public bool DeleteUser(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                var category = context.Login_tbl.FirstOrDefault(y => y.ID == id && y.IsDeleted != true);
                if (category != null)
                {
                    category.IsDeleted = true;
                    category.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                return true;
            }
        }

        public bool RegisterUser(Login_tbl Login_tbl)
        {
            try
            {
                using (var context = new UniversityEntities())
                {
                    context.Login_tbl.Add(Login_tbl);
                    context.SaveChanges();
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<Customer> GetCustomerList()
        {
            using (var context = new UniversityEntities())
            {
                return context.Customer.ToList();
            }
        }

        //    public bool AddOrUpdateCategory(CategoryMaster model)
        //    {
        //        using (var context = new UniversityEntities())
        //        {
        //            var category = context.CategoryMaster.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
        //            if(category!=null)
        //            {
        //                category.Name = model.Name;
        //                if (!string.IsNullOrWhiteSpace(model.ImageURL))
        //                {
        //                    category.ImageURL = model.ImageURL;
        //                }
        //                category.UpdatedDate = DateTime.UtcNow;
        //                context.SaveChanges();
        //            }
        //            else
        //            {
        //                model.CreatedDate = DateTime.UtcNow;
        //                context.CategoryMaster.Add(model);
        //                context.SaveChanges();
        //            }
        //            return true;
        //        }
        //    }

        //    public List<CategoryMaster> GetCategoryMasters()
        //    {
        //        using (var context = new UniversityEntities())
        //        {
        //            return context.CategoryMaster.Where(y => y.IsDeleted != true).ToList();
        //        }
        //    }

        //    public List<SmartSerach_Result> SmartSearch(string freeText)
        //    {
        //        using (var context = new UniversityEntities())
        //        {
        //            return context.SmartSerach(freeText).ToList();
        //        }
        //    }
    }

    
}
