using MakeYourHomework.AuthService.Data;
using MakeYourHomework.AuthService.Models;
using MakeYourHomework.AuthService.Repositories;
using MakeYourHomework.AuthService.Repositories.Abstraction;
using MakeYourHomework.AuthService.Services;
using MakeYourHomework.AuthService.Services.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<AuthDataContext>(opt =>
{
    //if (builder.Environment.IsDevelopment())
    //{
    //    opt.UseInMemoryDatabase("InMemoryDatabase");
    //}
    //else
    //{
        opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
    //}
});

services.AddIdentity<User, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AuthDataContext>();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();

services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config.GetSection("Jwt:ValidIssuer").Value,
            ValidAudience = config.GetSection("Jwt:ValidAudience").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Secret").Value))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();