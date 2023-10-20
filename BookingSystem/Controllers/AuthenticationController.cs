using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BookingSystem.Data.Repositories.Interface;

namespace BookingSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private static readonly AuthenticationProperties COOKIE_EXPIRES =
            new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true,
            };

        [HttpPost("signin")]
        public async Task<ActionResult> SignInPost(SigninData value)
        {
            var loginResult = await userRepository.AttemptLoginAsync(value.Email, value.Password);
            if (!loginResult.Success)
                return this.BadRequest("Login failed"); 

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, value.Email),
                new Claim(ClaimTypes.Name, loginResult.Name),
                new Claim(ClaimTypes.Role,  "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = COOKIE_EXPIRES;

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return this.Ok();
        }

        [HttpPost("signout")]
        public async Task<ActionResult> SignOutPost()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.Ok();
        }
    }

    public class SigninData
    {
        /// <summary>
        /// The email address for the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password for the user
        /// </summary>
        public string Password { get; set; }

    }
}
