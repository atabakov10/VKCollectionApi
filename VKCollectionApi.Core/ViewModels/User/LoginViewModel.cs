using System.ComponentModel.DataAnnotations;

namespace VKCollectionApi.Core.ViewModels.User
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "The Email field is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "The Password field is required.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
