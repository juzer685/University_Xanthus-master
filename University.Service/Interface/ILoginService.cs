using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Service.Interface
{
    public interface ILoginService 
    {
        Login_tbl UserLogin(Login_tbl login_Tbl);
        // List<Web_Login> GetLoginPage(Web_Login web_Login);
        // List<ProductUserGuide> GetSearchUserGuides(string SearchTxt);
        Login_tbl ForgotPassword(string Email);
        bool ChangePassword(string Email,string Password);
        Login_tbl CheckEmail(string Id, Func<string, string, bool> Func);
        EmailInfo AddEmailInfo(int UserId);
    }
}
