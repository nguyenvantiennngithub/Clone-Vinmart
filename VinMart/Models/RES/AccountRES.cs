using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VinMart.Models.RES
{
    public class AccountRES
    {
        [Required(ErrorMessage = "Please enter your user name!")]
        private string username;



        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        [Required(ErrorMessage = "Please enter your password!")]
        [DataType(DataType.Password)]
        private string password;
       
        [Required]
        [DataType(DataType.Password)]
        private string confirmpasword;
        [Required(ErrorMessage = "Please enter your displayname!")]
        private string displayName;
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]

        [Required(ErrorMessage = "Please enter your email!")]
        private string email;


        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string Email { get => email; set => email = value; }
        public string Confirmpasword { get => confirmpasword; set => confirmpasword = value; }
    }
}