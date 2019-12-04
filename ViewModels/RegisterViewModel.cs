using HotelManagement.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse",controller:"Account")]
       // [ValidationEmailDomain(allowDomain:"dotnetminds.com",ErrorMessage ="Email domain must be DotNetMinds.Com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}
