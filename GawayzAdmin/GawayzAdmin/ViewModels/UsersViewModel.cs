using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GawayzAdmin.ViewModels
{
    public class UsersViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4,ErrorMessage = "Password Must be between 4 and 8 characters")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4,ErrorMessage = "Password Must be between 4 and 8 characters")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm Password Doesn't Match password")]
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }
    }

    public class EditUserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }

    public class ChangePassword
    {
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}