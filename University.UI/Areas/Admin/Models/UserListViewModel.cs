using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace University.UI.Areas.Admin.Models
{
    public class UserListViewModel
    {
        //string CategoryImagePath = WebConfigurationManager.AppSettings["CategoryImagePath"];
        public Decimal Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}