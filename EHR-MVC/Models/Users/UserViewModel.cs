using System.ComponentModel.DataAnnotations;

namespace EHR_MVC.Models.Users
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        [Required(ErrorMessage = "Family name is required.")]
        public string FamilyName { get; set; }

        [Required(ErrorMessage = "Given name is required.")]
        public string GivenName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // public string Role { get; set; } 
    }
}
