using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Repository.Interface;

namespace University.Repository
{
    public class LoginRepository : ILoginRepository
    {
        //connectionstring = ConfigurationManager.ConnectionStrings["IPSU_DEV_V5"].ConnectionString;

        public Login_tbl UserLogin(Login_tbl login_Tbl)
        {
            using (var context = new UniversityEntities())
            {
                var t = context.Login_tbl.Where(y => y.UserName == login_Tbl.UserName && y.Password == login_Tbl.Password && y.IsDeleted != true).FirstOrDefault();
                return t;

            }
            //string consql = "data source=DESKTOP-M1MKH53\\SQLEXPRESS;initial catalog=IPSU_DEV_V5;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter();
            //int usercount;
            //try
            //{
            //    SqlConnection ConnectionString = new SqlConnection(consql);
            //    using (ConnectionString)
            //    {
            //        ConnectionString.Open();
            //        SqlCommand cmd = new SqlCommand("sp_LoginUniersity", ConnectionString);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@UserName", web_Login.UserName);
            //        cmd.Parameters.AddWithValue("@Password", web_Login.Password);
            //        // cmd.Parameters.AddWithValue("@RollName",web_Login.RoleName);

            //        using (da = new SqlDataAdapter(cmd))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            da.Fill(dt);
            //        }
            //        usercount = Convert.ToInt32(dt.Rows[0]["AuthenticationValue"]);
            //        ConnectionString.Close();
            //        if (usercount == 1)
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            ConnectionString.Close();
            //            return false;
            //        }


            //    }
            //    // return true;
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}
        }

        public Login_tbl ForgotPassword(string Email)
        {
            using (var context = new UniversityEntities())
            {
                //return context.Login_tbl.Where(y => y.UserName.Equals(Email)).Any();
                return context.Login_tbl.FirstOrDefault(y => y.UserName.Equals(Email) && y.IsDeleted != true);
            }
        }

        public bool ChangePassword(string Email, string Password)
        {
            try
            {
                using (var context = new UniversityEntities())
                {
                    Login_tbl Login_tbl = context.Login_tbl.Where(y => y.UserName.Equals(Email) && y.IsDeleted != true).FirstOrDefault();
                    Login_tbl.Password = Password;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Login_tbl CheckEmail(string Id, Func<string, string, string> Func)
        {
            using (var context = new UniversityEntities())
            {
                int ID = Convert.ToInt32(Func(Id, ConfigurationManager.AppSettings["SecurityKey"]));
                var EmailInfo = context.EmailInfoes.FirstOrDefault(y => y.ID == ID);
                var CreatedDate = (DateTime)EmailInfo.CreatedDate;
                if ((DateTime.Now.TimeOfDay - (TimeSpan)EmailInfo.SendTime).Duration() > TimeSpan.FromMinutes(30) || CreatedDate.Date != DateTime.Now.Date)
                {
                    return null;
                }
                else
                {
                    return context.Login_tbl.Where(x => x.ID == EmailInfo.UserId && x.IsDeleted != true).FirstOrDefault();
                }
            }
        }

        public EmailInfo AddEmailInfo(int UserId)
        {
            using (var context = new UniversityEntities())
            {
                EmailInfo EmailInfo = new EmailInfo
                {
                    SendTime = DateTime.Now.TimeOfDay,
                    UserId = UserId,
                    CreatedDate = DateTime.Now
                };
                context.EmailInfoes.Add(EmailInfo);
                context.SaveChanges();
                return EmailInfo;
            }
        }
    }
}
