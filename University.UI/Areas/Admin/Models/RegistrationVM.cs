using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using University.Data;
using University.UI.Models;

namespace University.UI.Areas.Admin.Models
{
    public class RegistrationVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter FirstName")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Special character should not be entered")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter LastName")]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Special character should not be entered")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter ConfirmPassword")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public int RoleID { get; set; }

        [Required(ErrorMessage = "Please enter Customer Id")]
        public decimal? CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter Mobile Number")]
        [RegularExpression("([0-9]+)",ErrorMessage ="Only Numbers are Allowed")]
        [StringLength(10, ErrorMessage = "Do not enter more than 10 Numbers")]
        public string MobileNo { get; set; }
        public List<Customer> CustomerList { get; set; }
    }
}