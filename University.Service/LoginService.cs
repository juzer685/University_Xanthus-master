using University.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Repository.Interface;
using University.Data;
using University.Repository;

namespace IPSU.Service
{
    public class LoginService : ILoginService
    {
        private ILoginRepository _LoginRepository;
        public LoginService()
        {
            _LoginRepository = new LoginRepository();
        }
        public Login_tbl UserLogin(Login_tbl login_Tbl)
        {
            var result = _LoginRepository.UserLogin(login_Tbl);
            return result;
        }

        public Login_tbl ForgotPassword(string Email)
        {
            return _LoginRepository.ForgotPassword(Email);
        }

        public bool ChangePassword(string Email,string Password)
        {
            return _LoginRepository.ChangePassword(Email,Password);
        }

        public Login_tbl CheckEmail(string Id, Func<string,string,string> Func)
        {
            return _LoginRepository.CheckEmail(Id, Func);
        }

        public EmailInfo AddEmailInfo(int UserId)
        {
            return _LoginRepository.AddEmailInfo(UserId);
        }
    }
}
