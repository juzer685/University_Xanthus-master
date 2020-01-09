using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Models
{
    [Serializable]
    public class UserModel
    {
        public int UserId = 0;
        public string UserName = string.Empty;
        public bool IsAdmin = false;
        public Guid? LoginSessionTokenValue = null;
        public string password { get; set; }
        public string userType { get; set; }
    }

}