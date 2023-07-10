using MakeYourHomework.AuthService.Models;

namespace MakeYourHomework.AuthService.Services.Abstraction;

public interface IJwtTokenService : ITokenService<User, string>
{
}
