using University.Data;
using University.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace University.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        public IEnumerable<SubCategoryMaster> GetSubCategoryList()
        {
            using (var context = new UniversityEntities())
            {
                int AssociatedUserID = Convert.ToInt32(HttpContext.Current.Session["UserSessionIDs"]);
                //if(AssociatedUserID==0)
                //{
                  
                //}
                    return context.SubCategoryMaster.Include("CategoryMaster").Where(y => y.IsDeleted != true && y.AssocitedID == AssociatedUserID).OrderByDescending(t => t.CreatedDate).ToList();
               
               
            }
        }
        public IEnumerable<SubCategoryMaster> GetSubCategoryListOnlyHaveProduct()
        {
            using (var context = new UniversityEntities())
            {
                int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                return context.SubCategoryMaster.Include("CategoryMaster").Where(y => y.IsDeleted != true && y.Product.Count>0 && y.AssocitedID==UserID).ToList();
            }
        }
        public SubCategoryMaster GetSubCategory(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                return context.SubCategoryMaster.Include("CategoryMaster").FirstOrDefault(y => y.Id == id && y.IsDeleted != true);
            }
        }

        public bool DeleteSubCategory(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                var subcategory = context.SubCategoryMaster.FirstOrDefault(y => y.Id == id && y.IsDeleted != true);
                if (subcategory != null)
                {
                    subcategory.IsDeleted = true;
                    subcategory.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                return true;
            }
        }

        public bool AddOrUpdateSubCategory(SubCategoryMaster model)
        {
            using (var context = new UniversityEntities())
            {
                var subcategory = context.SubCategoryMaster.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted != true);
                var categoryId = context.CategoryMaster.FirstOrDefault().Id;
                if (subcategory != null)
                {
                    if (!string.IsNullOrWhiteSpace(model.ImageURL))
                    {
                        subcategory.ImageURL = model.ImageURL;
                    }
                    subcategory.Name = model.Name;
                    subcategory.CategoryId = categoryId;
                    subcategory.UpdatedDate = DateTime.UtcNow;
                    subcategory.ImageALT = model.ImageALT;
                    context.SaveChanges();
                }
                else
                {
                    model.CategoryId = categoryId;
                    model.CreatedDate = DateTime.UtcNow;
                    model.IsDeleted = false;
                    context.SubCategoryMaster.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
        }

        public List<SubCategoryMaster> GetSubCategoryListById(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                
                return context.SubCategoryMaster.Where(y => y.CategoryId == id && y.IsDeleted != true ).ToList();
            }
        }

        public IEnumerable<SubCategoryMaster> GetSubCategoryList(Decimal CategoryId)
        {
            using (var context = new UniversityEntities())
            {
                return context.SubCategoryMaster.Include("CategoryMaster").Where(y => y.CategoryId == CategoryId && y.IsDeleted != true).ToList();
            }
        }
    }
}