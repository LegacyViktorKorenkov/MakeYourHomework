using Microsoft.AspNetCore.Identity;

namespace MakeYourHomework.AuthService.Models;

public class User : IdentityUser
{
    public string Nickname { get; set; }

    public UserType UserType { get; set; }
}
