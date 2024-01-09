using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//JWT
 //Add authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mySecretKey")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


//Cookie
// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Test"; // Name of the authentication cookie
        options.Cookie.HttpOnly = true; // The cookie cannot be accessed by JavaScript
        options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest; // Cookie requires HTTPS
        options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict; // Restrict cookie to the same origin
        options.Cookie.IsEssential = true; // Indicates that the cookie is essential for the application's operation
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Expiration time for the cookie
        options.SlidingExpiration = true; // Renew the cookie expiration time on each request
        //options.LoginPath = "/Account/Login"; // Redirect users to the login page if authentication is required
    });



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//Basic Auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();