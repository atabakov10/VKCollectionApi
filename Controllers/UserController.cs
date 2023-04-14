using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VKCollectionApi.Data.Models;
using VKCollectionApi.ViewModels.User;

namespace VKCollectionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly SignInManager<Client> _signInManager;
        private readonly UserManager<Client> _userManager;

        public UserController(SignInManager<Client> signInManager,
                                UserManager<Client> userManager
                               )
        {
            _signInManager = signInManager;
            _userManager = userManager;
           
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var user = new Client
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName= model.FirstName,
                LastName= model.LastName,
                PhoneNumber= model.PhoneNumber,
            };


            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                

                return Ok(new {_Id = user.Id, Username = user.UserName, UserEmail = user.Email });

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
                var user = await _userManager.FindByEmailAsync(model.Email);
               

                return Ok(new { _Id = user.Id , UserEmail = model.Email });
            }
            else
            {
                return BadRequest("Invalid email or password");
            }
        }
    }
}
