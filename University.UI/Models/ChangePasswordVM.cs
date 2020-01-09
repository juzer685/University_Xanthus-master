using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.UI.Models
{
    public class ChangePasswordVM
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
