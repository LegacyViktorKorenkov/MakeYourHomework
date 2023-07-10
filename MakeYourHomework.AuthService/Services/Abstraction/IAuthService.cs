using MakeYourHomework.AuthService.Models;

namespace MakeYourHomework.AuthService.Services.Abstraction;

public interface IAuthService
{
    Task<AuthResponse> SignUpAsync(SignupModel signupModel);
    Task<AuthResponse> SignInAsync(SigninModel signinModel);
    Task Logout();
}
