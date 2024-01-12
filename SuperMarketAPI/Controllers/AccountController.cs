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

        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Validate the received credentials (for example, against a database)
            bool isValidCredentials = ValidateCredentials(loginModel.Username, loginModel.Password);

            if (isValidCredentials)
            {
                // Create claims based on the validated user
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, loginModel.Username)
                    // Add other claims as needed (e.g., roles, additional information)
                };
                var secretKey = _configuration["Jwt:SecretKey"];
                if (string.IsNullOrEmpty(secretKey))
                {
                    // Handle the case where the configuration value is missing or empty
                    throw new InvalidOperationException("Jwt:SecretKey is missing or empty in the configuration.");
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(30), // Token expiration time
                        signingCredentials: creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized();
        }

        // Method to validate credentials (replace with actual validation logic)
        private bool ValidateCredentials(string username, string password)
        {
            // Replace this with your actual validation logic against a user database or other source
            return username == "Dimitask" && password == "9963";
        }

        #endregion


        #region Cookie

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginModel loginModel)
        //{
        //    // Validate the received credentials (replace with your authentication logic)
        //    bool isValidCredentials = ValidateCredentials(loginModel.Username, loginModel.Password);

        //    if (isValidCredentials)
        //    {
        //        // Generate a simple authentication token (for demonstration purposes)
        //        string authToken = Guid.NewGuid().ToString();

        //        // Set the authentication cookie on successful authentication
        //        Response.Cookies.Append("AuthCookie", authToken, new CookieOptions
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
        //    return username == "DimTask" && password == "9963";
        //}

        #endregion

    }
}
