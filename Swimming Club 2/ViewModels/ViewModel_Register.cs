using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Swimming_Club_2.Models
{
    public class ViewModel_Register
    {
        [Required]
        [EmailAddress]
        [Microsoft.AspNetCore.Mvc.Remote(controller: "account", action: "IsEmailInUse")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Confirm password")]
        [Compare("Password", ErrorMessage ="Passwords must match!")]
        public string  ConfirmPassword { get; set; }
      
}
}
