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
        bool ForgotPassword(string Email);
        bool ChangePassword(string Email,string Password);
        string CheckEmail(string Email, Func<string, string, bool> Func);
    }
}
