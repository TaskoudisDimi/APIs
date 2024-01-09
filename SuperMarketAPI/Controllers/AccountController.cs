using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SuperMarketAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperMarketAPI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region JWT
       
        //private readonly IConfiguration _configuration;

        //public AccountController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginModel loginModel)
        //{
        //    // Validate the received credentials (for example, against a database)
        //    bool isValidCredentials = ValidateCredentials(loginModel.Username, loginModel.Password);

        //    if (isValidCredentials)
        //    {
        //        // Create claims based on the validated user
        //        var claims = new[]
        //        {
        //        new Claim(ClaimTypes.Name, loginModel.Username)
        //        // Add other claims as needed (e.g., roles, additional information)
        //    };

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Issuer"],
        //            audience: _configuration["Jwt:Audience"],
        //            claims: claims,
        //            expires: DateTime.UtcNow.AddMinutes(30), // Token expiration time
        //            signingCredentials: creds
        //        );

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token)
        //        });
        //    }

        //    return Unauthorized();
        //}

        //// Method to validate credentials (replace with actual validation logic)
        //private bool ValidateCredentials(string username, string password)
        //{
        //    // Replace this with your actual validation logic against a user database or other source
        //    return username == "validUser" && password == "validPassword";
        //}

        #endregion


        #region Cookie

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginModel loginModel)
        //{
        //    // Validate the received credentials (replace with your authentication logic)
        //    bool isValidCredentials = ValidateCredentials(loginModel.Username, loginModel.Password);

        //    if (isValidCredentials)
        //    {
        //        // Set the authentication cookie on successful authentication
        //        Response.Cookies.Append("AuthCookie", "Authenticated", new CookieOptions
        //        {
        //            HttpOnly = true, // Make the cookie accessible only via HTTP (not JavaScript)
        //            Expires = DateTime.UtcNow.AddMinutes(30) // Set expiration time for the cookie
        //        });

        //        return Ok("Successfully authenticated and set cookie.");
        //    }

        //    return Unauthorized("Invalid credentials.");
        //}

        //// Method to validate credentials (replace with your actual validation logic)
        //private bool ValidateCredentials(string username, string password)
        //{
        //    // Replace this with your actual validation logic against a user database or other source
        //    return username == "validUser" && password == "validPassword";
        //}

        #endregion

    }
}
