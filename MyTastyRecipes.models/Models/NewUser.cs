using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTastyRecipes.Models
{
    public class NewUser
    {
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        public string Email { get; set; }
        [Required, MinLength(6, ErrorMessage = "Password requires minimum of 6 characters")]
        public string Password { get; set; }
        public string Salt { get; set; }
        public string EncryptedPass { get; set; }
    }
}