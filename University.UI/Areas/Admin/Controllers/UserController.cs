using University.Data;
using University.Service;
using University.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using University.Service.Interface;
using University.UI.Models;
using System.Web.Security;
using University.UI.Utilities;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace University.UI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private IUseradminService _UseradminService;
        // string CategoryImagePath = WebConfigurationManager.AppSettings["CategoryImagePath"];

        public UserController()
        {
            _UseradminService = new UseradminService();
        }
        // GET: Admin/Category
        public ActionResult index()
        {
            var res = _UseradminService.GetUserList().ToList();
            var viewModel = AutoMapper.Mapper.Map<List<Login_tbl>, List<Login_tbl>>(res);
            Session["UserList"] = viewModel;
            return View(viewModel);
        }

        //public ActionResult AddEditCategory(Login_tbl model)
        //{
        //var res = AutoMapper.Mapper.Map<Login_tbl>(model);
        // var isSuccess = _UseradminService.AddOrUpdateCategory(res);
        //  return Json(isSuccess, JsonRequestBehavior.AllowGet);
        //    return View();
        //}

        //private string UploadFileOnServer(string location, HttpPostedFileBase file)
        //{
        //    string extension = Path.GetExtension(file.FileName);
        //    string fileId = Guid.NewGuid().ToString().Replace("-", "");
        //    string filename = fileId + extension;
        //    var path = Path.Combine(Server.MapPath(location), filename);
        //    file.SaveAs(path);
        //    return filename;
        //}
        //public ActionResult GetCategory(string Id)
        //{
        //    CategoryViewModel model;
        //    if (!string.IsNullOrEmpty(Id))
        //    {
        //        var res = _categoryMasterService.GetCategory(Convert.ToDecimal(Id));
        //        model = AutoMapper.Mapper.Map<CategoryMaster, CategoryViewModel>(res);
        //    }
        //    else
        //    {
        //        model = new CategoryViewModel();
        //    }

        //    return PartialView("AddEditCategory",model);
        //}

        //[HttpDelete]
        public ActionResult DeleteUser(string Id)
        {
            var res = _UseradminService.DeleteUser(Convert.ToDecimal(Id));
            return Json(new { url = "/Admin/User" });
            //  return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Register()
        {
            var res = _UseradminService.GetCustomerList();
            if (TempData["EditUserId"] != null)
            {
                //int id = Convert.ToInt32(TempData["EditUserId"]);
                Login_tbl Login_tbl = _UseradminService.EditUser(Convert.ToInt32(TempData["EditUserId"]));
                return View("Register", new RegistrationVM
                {
                    ID = Login_tbl.ID,
                    FirstName = Login_tbl.FirstName,
                    LastName = Login_tbl.LastName,
                    Email = Login_tbl.UserName,
                    MobileNo = Login_tbl.MobileNo,
                    CustomerId = Login_tbl.CustomerId,
                    CustomerList = res
                });
            }
            else
            {
                return View("Register", new RegistrationVM { CustomerList = res });
            }
        }

        [HttpPost]
        public ActionResult Register(RegistrationVM p_RegistrationVM)
        {
            var res = _UseradminService.RegisterUser(new Login_tbl
            {
                FirstName = p_RegistrationVM.FirstName,
                LastName = p_RegistrationVM.LastName,
                UserName = p_RegistrationVM.Email,
                Password = new MD5Hashing().GetMd5Hash(p_RegistrationVM.Password), //p_RegistrationVM.Password,
                CustomerId = p_RegistrationVM.CustomerId,
                MobileNo = p_RegistrationVM.MobileNo,
                RoleID = 5,
                CreatedDate = DateTime.Now,
                CreatedBy = Convert.ToDecimal(Session["RoleID"]),
                IsDeleted = false,
                AdminId = Convert.ToInt32(Session["AdminLoginID"])
            });
            if (res.Item1 == true)
            {
                bool success = SendEmail(p_RegistrationVM.Email, p_RegistrationVM.Password, p_RegistrationVM.FirstName);
                if (success)
                {
                    return Json(new { result = true, Message = "User Registration Successful and Email Sent", url = "/Admin/User" });
                }
                else
                {
                    return Json(new { result = true, Message = "User Registration Successful but Email not sent", url = "/Admin/User" });
                }
            }
            else
            {
                if (res.Item2 == true)
                {
                    return Json(new { result = false, Message = "Email already Exists.", url = "/Admin/User/Register" });
                }
                else
                {
                    return Json(new { result = false, Message = "something went wrong,please try again.", url = "/Admin/User/Register" });
                }
            }
        }

        public bool SendEmail(string p_Email, string p_Password, string FirstName)
        {
            try
            {
                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["Host"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["AdminId"], ConfigurationManager.AppSettings["AdminPassword"])
                };
                using (var message = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["AdminId"], "Admin"), new MailAddress(p_Email, "user"))
                {
                    Subject = "Login Credentials",
                    Body = "Hello " + FirstName + ",<br/><br/>Welcome to Online Training Portal.Below are the login credentials for the system <br/><br/>Username: " + p_Email + " <br/>Password: " + p_Password + "<br/><br/>Please <a href=" + ConfigurationManager.AppSettings["LoginUrl"] + ">Click Here</a> to login <br/><br/><br/>Regards,<br/>Admin"
                })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            TempData["EditUserId"] = id;
            return RedirectToAction("Register");
        }
    }
}
