using System.ComponentModel.DataAnnotations;

namespace MakeYourHomework.AuthService.Models;

public class SignupModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Nickname { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
    [Required]
    public UserType UserType { get; set; }
}
