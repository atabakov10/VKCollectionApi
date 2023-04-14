using System.ComponentModel.DataAnnotations;

namespace VKCollectionApi.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The Username field is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The First Name field is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
