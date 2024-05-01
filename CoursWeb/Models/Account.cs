using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoursWeb.Models
{
    public class Account
    {
        [ScaffoldColumn(false)]
        public int AccountID { get; set; }

        [Required, StringLength(100), Display(Name = "Username")]
        public string Username { get; set; }

        [Required, StringLength(100)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool isAdmin { get; set; }
    }
}