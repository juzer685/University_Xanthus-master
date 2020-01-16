using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;

namespace University.Repository.Interface
{
    public interface ILoginRepository
    {
        Login_tbl UserLogin(Login_tbl login_Tbl);
        Login_tbl ForgotPassword(string Email);
        bool ChangePassword(string Email,string Password);
        Login_tbl CheckEmail(string Id, Func<string, string, bool> Func);
        EmailInfo AddEmailInfo(int UserId);
    }
}
