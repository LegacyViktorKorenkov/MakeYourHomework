using MakeYourHomework.AuthService.Dtos;
using MakeYourHomework.AuthService.Models;
using System.Linq.Expressions;

namespace MakeYourHomework.AuthService.Services.Abstraction;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync(Expression<Func<User, bool>> filter = null);
    Task<UserDto> GetUserByIdAsync(string id);
    Task<UserDto> GetUserByNicknameOrEmailAsync(string param);
    Task<bool> UserWithEmailExists(string email);
    Task<bool> UserWithNicknameExists(string nickname);
}
