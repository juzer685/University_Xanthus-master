﻿using University.Data;
using University.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using University.Core;

namespace University.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        //public List<CategoryUserMapping> GetCategoryUservideoMappingList()
        //{
        //    using (var context = new UniversityEntities())
        //    {
        //        List<SubCategoryMaster> obj2 = context.SubCategoryMaster.Include("CategoryMaster").Where(y => y.IsDeleted != true && y.AssocitedCustID == AdminId).OrderByDescending(t => t.CreatedDate).ToList();
        //    }
        //}
        public IEnumerable<SubCategoryMaster> GetCategoryUserList(decimal test)
        {
            using (var context = new UniversityEntities())
            {
                int AdminId = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                var res = (from cm in context.CategoryUserMapping.Where(y => y.UserID == test && y.IsDeleted == false)
                           join s in context.SubCategoryMaster.Where(y => y.IsDeleted == false)
                           on cm.CategoryID equals s.Id
                           join l in context.Login_tbl.Where(y => y.IsDeleted == false && y.ID == test)
                           on cm.UserID equals l.ID
                           orderby cm.CreatedDate
                           select new
                           {
                               //categorylist = context.SubCategoryMaster.Where(y => y.IsDeleted == false).ToList(),
                               // ProductVideos = context.CategoryUserMapping.Where(y => y.IsDeleted != true).ToList(),
                               cm.UserID,
                               s.Name,
                               cm.CategoryID
                           }).ToList();
                var categorylist = context.SubCategoryMaster.Where(y => y.IsDeleted == false).ToList();

               
                var finalcategory = categorylist.Where(x => res.Select(d => d.Name).Contains(x.Name)).ToList();
                var finalcategorylist = categorylist.Except(finalcategory);
                //ar finalcategorylist = res.ToList();
                //CategoryUserMapping categoryUserMappings = new CategoryUserMapping();

                //List<CategoryUserMapping> categoryUserMappings1 = new List<CategoryUserMapping>();
                ////var categoryUserMappings1 = new lis
                //foreach (var subcategory in finalcategorylist)
                //{
                //    categoryUserMappings1.Add(new CategoryUserMapping
                //    {

                //        CategoryID = subcategory.CategoryId,
                //        CategoryName = subcategory.Name
                //    });



                //}

                return finalcategorylist;
            }


        }

        public List<CategoryModel> BindCategories(decimal UserID)
        {
            List<CategoryModel> BindCategories = new List<CategoryModel>();
            using (var context = new UniversityEntities())
            {


                var UserCategories = (from cat in context.SubCategoryMaster
                                      join catmap in context.CategoryUserMapping
                                      on cat.Id equals catmap.CategoryID
                                      where catmap.UserID == UserID && cat.IsDeleted != true
                                      select new CategoryModel
                                      {
                                          CategoryID = cat.Id,
                                      }).ToList();

                List<CategoryModel> Categories = (from cat in context.SubCategoryMaster
                                                  where cat.IsDeleted != true
                                                  select new CategoryModel
                                                  {
                                                      CategoryID = cat.Id,
                                                      CategoryName = cat.Name
                                                  }).ToList();

                HashSet<decimal> UserCategoryIds = new HashSet<decimal>(UserCategories.Select(s => s.CategoryID));
                BindCategories = Categories.Where(m => !UserCategoryIds.Contains(m.CategoryID)).ToList();
                return BindCategories;
            }


        }

        public bool DeleteCategoryUseerMapping(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                var categoryUserMapping = context.CategoryUserMapping.FirstOrDefault(y => y.ID == id && y.IsDeleted != true);
                if (categoryUserMapping != null)
                {
                    categoryUserMapping.IsDeleted = true;
                    categoryUserMapping.DeletedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                return true;
            }
        }
        public (List<Login_tbl>,List<SubCategoryMaster>) GetCategoryUserMappingList()
        {
            using (var context = new UniversityEntities())
            {
                
                int AdminId = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);

                List<Login_tbl> obj1 = context.Login_tbl.Where(y => y.IsDeleted == false && y.AdminId == AdminId).ToList();
                List<SubCategoryMaster> obj2 = context.SubCategoryMaster.Include("CategoryMaster").Where(y => y.IsDeleted == false && y.AssocitedCustID == AdminId).OrderByDescending(t => t.CreatedDate).ToList();

             

                return (obj1,obj2);


            }

        }
        public List<CategoryUserMapping> GetCategoryUserMappingGrid()
        {
            using (var context = new UniversityEntities())
            {
                int AdminId = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                if (AdminId != 0)
                {
                    var Obj = context.CategoryUserMapping
                        .Where(x => x.AdminID == AdminId && x.IsDeleted == false)
                        .Select(x => new {  id=x.ID,x.Login_tbl.ID, x.Login_tbl.FirstName, x.Login_tbl.LastName, x.Login_tbl.AdminId, x.SubCategoryMaster.Id, x.SubCategoryMaster.Name, x.SubCategoryMaster.IsDeleted })
                        .ToList()
                        .Select(x => new CategoryUserMapping
                        {
                            ID=x.id,
                            UserID = x.ID,
                            UserFirstName = x.FirstName,
                            UserLastName = x.LastName,
                            AdminID = x.AdminId,
                            CategoryID = x.Id,
                            CategoryName = x.Name,
                            CategoryDeleteorNot = x.IsDeleted,

                        }).ToList();
                    var AdminDetails = context.Login_tbl.FirstOrDefault(x => x.ID == AdminId && x.IsDeleted==false);
                    foreach (var item in Obj)
                    {
                        item.AdminFirstName = AdminDetails.FirstName;
                        item.AdminLastName = AdminDetails.LastName;
                    }
                    return Obj;
                }
                else
                {
                    int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                    var Obj = context.CategoryUserMapping
                       .Where(x => x.UserID == UserID && x.IsDeleted != true)
                       .Select(x => new { x.Login_tbl.ID, x.Login_tbl.FirstName, x.Login_tbl.LastName, x.Login_tbl.AdminId, x.SubCategoryMaster.Id, x.SubCategoryMaster.Name,x.SubCategoryMaster.IsDeleted })
                       .ToList()
                       .Select(x => new CategoryUserMapping
                       {
                           ID = x.ID,
                           UserID = x.ID,
                           UserFirstName = x.FirstName,
                           UserLastName = x.LastName,
                           AdminID = x.AdminId,
                           CategoryID = x.Id,
                           CategoryName = x.Name,
                           CategoryDeleteorNot=x.IsDeleted 



                       }).ToList();
                    var AdminDetails = context.Login_tbl.FirstOrDefault(x => x.ID == UserID);
                    foreach (var item in Obj)
                    {
                        item.AdminFirstName = AdminDetails.FirstName;
                        item.AdminLastName = AdminDetails.LastName;
                    }
                    return Obj;

                }
            }
        }

        public IEnumerable<SubCategoryMaster> GetSubCategoryList()
        {
            using (var context = new UniversityEntities())
            {
                int AdminId = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                if (AdminId != 0)
                {
                    return context.SubCategoryMaster.Include("CategoryMaster").Where(y => y.IsDeleted == false && y.AssocitedCustID == AdminId).OrderByDescending(t => t.CreatedDate).ToList();
                }
                else
                {
                    return context.SubCategoryMaster.Include("CategoryMaster").Where(y => y.IsDeleted == false).OrderByDescending(t => t.CreatedDate).ToList();
                }




            }
        }
        public IEnumerable<SubCategoryMaster> GetSubCategoryListOnlyHaveProduct()
        {
            using (var context = new UniversityEntities())
            {
                //int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                //return context.SubCategoryMaster.Include("CategoryMaster").Where(y => y.IsDeleted != true && y.Product.Count > 0).ToList();
                int UserID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);

                var res = (from l in context.Login_tbl.Where(y => y.IsDeleted != true && y.ID == UserID)
                           join cm in context.CategoryUserMapping.Where(y => y.IsDeleted != true && y.UserID == UserID)
                           on l.ID equals cm.UserID
                           join c in context.SubCategoryMaster.Where(y => y.IsDeleted != true)
                           on cm.CategoryID equals c.Id
                          // join p in context.Product.Where(y => y.IsDeleted != true)
                          // on c.Id equals p.SubCategoryId
                          // join pv in context.ProductVideos.Where(y => y.IsDeleted != true)
                          // on p.Id equals pv.ProductId
                           orderby c.CreatedDate
                           select new
                           {
                               c.Id,
                               c.Name,
                               c.ImageURL
                              
                           }

                           ).ToList();
                List<SubCategoryMaster> subCategoryMasters  = new List<SubCategoryMaster>();
                foreach (var subcategory in res)
                {
                    subCategoryMasters.Add(new SubCategoryMaster
                    {
                        Id = subcategory.Id,
                        Name = subcategory.Name,
                        ImageURL = subcategory.ImageURL
                        //VideoURL = subcategory.VideoURL

                    });
                    
                }
                return subCategoryMasters;
            }
        }

        public CategoryUserMapping GetCategoryUserMapping(Decimal CatMappid)
        {
            using (var context = new UniversityEntities())
            {
                var Obj = context.CategoryUserMapping
                       .Where(x => x.ID == CatMappid && x.IsDeleted != true)
                       .Select(x => new { id=x.ID,x.Login_tbl.ID, x.Login_tbl.FirstName, x.Login_tbl.LastName, x.Login_tbl.AdminId, x.SubCategoryMaster.Id, x.SubCategoryMaster.Name })
                       .FirstOrDefault();
                return new CategoryUserMapping
                {
                    ID = Obj.id,
                    UserID=Obj.ID,
                    UserFirstName = Obj.FirstName,
                    UserLastName = Obj.LastName,
                    AdminID = Obj.AdminId,
                    CategoryID = Obj.Id,
                    CategoryName = Obj.Name
                };
                //return context.CategoryUserMapping.FirstOrDefault(y => y.ID == id && y.IsDeleted != true);
            }
        }
        public SubCategoryMaster GetSubCategory(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                return context.SubCategoryMaster.Include("CategoryMaster").FirstOrDefault(y => y.Id == id && y.IsDeleted == false);
            }
        }

        public bool DeleteSubCategory(Decimal id)
        {
            using (var context = new UniversityEntities())
            {
                var subcategory = context.SubCategoryMaster.FirstOrDefault(y => y.Id == id && y.IsDeleted == false);
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
                var subcategory = context.SubCategoryMaster.FirstOrDefault(y => y.Id == model.Id && y.IsDeleted == false);
                var categoryId = context.CategoryMaster.FirstOrDefault().Id;
                if (subcategory != null)
                {
                    if (!string.IsNullOrWhiteSpace(model.ImageURL))
                    {
                        subcategory.ImageURL = model.ImageURL;
                    }
                    subcategory.AssocitedCustID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                    subcategory.Name = model.Name;
                    subcategory.CategoryId = categoryId;
                    subcategory.UpdatedDate = DateTime.UtcNow;
                    subcategory.ImageALT = model.ImageALT;
                    context.SaveChanges();
                }
                else
                {
                    model.AssocitedCustID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                    model.CategoryId = categoryId;
                    model.CreatedDate = DateTime.UtcNow;
                    model.IsDeleted = false;
                    context.SubCategoryMaster.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
        }
        public bool AddCategoryUserMapping(CategoryUserMapping model)
        {
            using (var context = new UniversityEntities())
            {
                var categoryMapping = context.CategoryUserMapping.FirstOrDefault(y => y.ID == model.ID && y.IsDeleted == false);
                // var categoryId = context.CategoryMaster.FirstOrDefault().Id;
                if (categoryMapping != null)
                {
                    categoryMapping.AdminID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                    categoryMapping.UserID = model.UserID;
                    categoryMapping.CategoryID = model.CategoryID;
                    categoryMapping.UpdatedDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                else
                {
                    model.AdminID = Convert.ToInt32(HttpContext.Current.Session["AdminLoginID"]);
                    //model.CategoryID = categoryId;
                    model.CreatedDate = DateTime.UtcNow;
                    model.IsDeleted = false;
                    context.CategoryUserMapping.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
        }


        public List<SubCategoryMaster> GetSubCategoryListById(Decimal id)
        {
            using (var context = new UniversityEntities())
            {

                return context.SubCategoryMaster.Where(y => y.CategoryId == id && y.IsDeleted != true).ToList();
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