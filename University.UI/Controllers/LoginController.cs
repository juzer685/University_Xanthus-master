using University.Data;
using University.Service.Interface;
using University.UI.Areas.Admin.Models;
using University.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University.Service;
using IPSU.Service;
using System.Web.Security;
using System.Web.Routing;
using University.UI.Utilities;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using University.UI.Models;

namespace IPSU.Web.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        // IPSU_DEV_V5Entities iPSU_DEV_V5Entities = new IPSU_DEV_V5Entities();
        private ILoginService _loginService;
        private IUseradminService _UseradminService;
        public LoginController()
        {

            _loginService = new LoginService();
            _UseradminService = new UseradminService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Login(Login_tbl login_Tbl)
        {
            login_Tbl.Password = new MD5Hashing().GetMd5Hash(login_Tbl.Password);
            var Result = _loginService.UserLogin(login_Tbl);
            if (Result == null)
            {
                TempData["Success"] = "False";
                TempData["Message"] = "You are not a valid user...Please Register..!!";
                return View();
            }
            else if (Result.RoleID == 4)
            {
                FormsAuthentication.SetAuthCookie(Result.UserName, false);
                Session["AdminLoginID"] = Result.ID;
                Session["RoleID"] = Result.RoleID;
                Session["AdminName"] = Result.FirstName + " " + Result.LastName;
                var res = _UseradminService.GetUserList().ToList();
                var viewModel = AutoMapper.Mapper.Map<List<Login_tbl>, List<Login_tbl>>(res);
                Session["UserList"] = viewModel;
                //  Session["UserLoginID"] = viewModel;
                return RedirectToAction("Index", "Home");
            }
            else if (Result.RoleID == 5)
            {
                FormsAuthentication.SetAuthCookie(Result.UserName, false);
                Session["UserLoginID"] = Result.ID;
                Session["RoleID"] = Result.RoleID;
                Session["UserNamee"] = Result.FirstName + " " + Result.LastName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "You are not a valid user...Please Register..!!";
                return View();
            }
            // return View();

        }
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();// it will clear the session at the end of request
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            Login_tbl Login_tbl = _loginService.ForgotPassword(Email);
            if (Login_tbl != null)
            {
                var EmailInfo = _loginService.AddEmailInfo(Login_tbl.ID);
                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["Host"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["AdminId"], ConfigurationManager.AppSettings["AdminPassword"])
                };
                using (var message = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["AdminId"], "Admin"), new MailAddress(Email, "user"))
                {
                    Subject = "Reset Password",
                    Body = "Hello,<br/><br/>Welcome to IPS University. We received a request to reset your password.Please Click <a href=" + ConfigurationManager.AppSettings["ChangePasswordUrl"] + "/" + new MD5Hashing().GetMd5Hash(EmailInfo.ID.ToString()) + ">Here</a> to reset Password <br/><br/><br/>Regards<br/>Admin"
                })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
                TempData["Success"] = "True";
                TempData["Message"] = "please check your email to change password";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["Success"] = "False";
                TempData["Message"] = "Email does not exist.Please contact Admin for registration";
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult ChangePassword(string id)
        {
            Login_tbl Login_tbl = _loginService.CheckEmail(id, new MD5Hashing().VerifyMd5Hash);
            if (Login_tbl == null)
            {
                TempData["Success"] = "False";
                TempData["Message"] = "Your link has Expired";
                return RedirectToAction("Login");
            }
            else
            {
                Session["UnEncryptedEmail"] = Login_tbl.UserName;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVM ChangePasswordVM)
        {
            if (ChangePasswordVM.NewPassword.Equals(ChangePasswordVM.ConfirmPassword))
            {
                var Result = _loginService.ChangePassword(Session["UnEncryptedEmail"].ToString(), new MD5Hashing().GetMd5Hash(ChangePasswordVM.NewPassword));
                if (Result)
                {
                    return Json(new { Message = "Change Password Successful", url = "/Login/Login" });
                }
                else
                {
                    return Json(new { Message = "Change Password UnSuccessful", url = "/Login/Login" });
                }
            }
            else
            {
                return Json(new { Message = "New Password and Confirm Password do not match.Please try again", url = "/Login/Login" });
            }
        }
    }
}
