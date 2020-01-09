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
    public class CategoryRepository: ICategoryRepository
    {

        public  IEnumerable<CategoryMaster> GetCategoryList()
        {
            using (var context = new UniversityEntities())
            {
                return context.CategoryMaster.Where(y => y.IsDeleted != true).ToList();
            }
        }
        public CategoryMaster GetCategory(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                return context.CategoryMaster.FirstOrDefault(y=>y.Id== id && y.IsDeleted != true);
            }
        }
        
        public bool DeleteCategory(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                var category = context.CategoryMaster.FirstOrDefault(y => y.Id == id && y.IsDeleted != true);
                if (category != null)
                {
                    category.IsDeleted = true;
                    category.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                return true;
            }
        }
       
        public bool AddOrUpdateCategory(CategoryMaster model)
        {
            using (var context = new UniversityEntities())
            {
                var category = context.CategoryMaster.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
                if(category!=null)
                {
                    category.Name = model.Name;
                    if (!string.IsNullOrWhiteSpace(model.ImageURL))
                    {
                        category.ImageURL = model.ImageURL;
                    }
                    category.UpdatedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                else
                {
                    model.CreatedDate = DateTime.UtcNow;
                    context.CategoryMaster.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
        }

        public List<CategoryMaster> GetCategoryMasters()
        {
            using (var context = new UniversityEntities())
            {
                return context.CategoryMaster.Where(y => y.IsDeleted != true).ToList();
            }
        }

        public List<SmartSerach_Result> SmartSearch(string freeText)
        {
            using (var context = new UniversityEntities())
            {
                return context.SmartSerach(freeText).ToList();
            }
        }
    }

    
}
