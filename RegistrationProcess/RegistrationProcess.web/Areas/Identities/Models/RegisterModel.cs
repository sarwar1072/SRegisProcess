using Membership.Entities;
using Membership.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Identities.Models
{
    //only SuperAdmin can register and will see registration button
    public class RegisterModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IUserService _userService;
        public RegisterModel()
        {
        }
        public RegisterModel(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }
        [Required]
        [Display(Name ="Name")]
        public string UserName { get; set; }       
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }       
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }


        //public async Task RegistrationAsync()
        //{
        //    var user = new ApplicationUser
        //    {
        //        UserName = this.UserName,
        //        PhoneNumber =this.PhoneNumber,
        //        Email = this.Email
        //    };
        //     await _userManager.CreateAsync(user);
        //}
    }
}
