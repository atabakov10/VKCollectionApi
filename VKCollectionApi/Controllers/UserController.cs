using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VKCollectionApi.Core.ViewModels.User;
using VKCollectionApi.Infrastructure.Data.Models;

namespace VKCollectionApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : Controller
	{


		private readonly SignInManager<Client> _signInManager;
		private readonly UserManager<Client> _userManager;
		private readonly IConfiguration _config;

		public UserController(SignInManager<Client> signInManager,
							   UserManager<Client> userManager,
							   IConfiguration config)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_config = config;
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
		{
			var user = new Client
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				PhoneNumber = model.PhoneNumber,
			};


			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, isPersistent: false);

				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:Key"));
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
				new Claim(ClaimTypes.Name, user.Id)
					}),
					Expires = DateTime.UtcNow.AddDays(7),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				var tokenString = tokenHandler.WriteToken(token);

				return Ok(new { Token = tokenString, _Id = user.Id, Username = user.UserName, UserEmail = user.Email });
			}
			else
			{
				return BadRequest(result.Errors);
			}
		}



		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginViewModel model)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

			if (result.Succeeded)
			{
				var user = await _userManager.FindByNameAsync(model.Email);

				// Generate JWT token
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:Key"));
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
				new Claim(ClaimTypes.Name, user.Id)
					}),
					Expires = DateTime.UtcNow.AddDays(7),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				var tokenString = tokenHandler.WriteToken(token);

				return Ok(new { Token = tokenString, _Id = user.Id, Username = user.UserName, UserEmail = user.Email });
			}
			else
			{
				return BadRequest("Invalid username or password");
			}
		}

	}
}
