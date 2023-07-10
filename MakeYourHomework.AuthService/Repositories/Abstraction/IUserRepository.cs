using MakeYourHomework.AuthService.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MakeYourHomework.AuthService.Repositories.Abstraction;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> filter = null);
    Task<User> GetAsync(
        Expression<Func<User, bool>> filter = null, 
        Func<IQueryable<User>, IIncludableQueryable<User, object>> includes = null);
    Task<bool> UserExistAsync(Expression<Func<User, bool>> filter);

    Task SaveChangesAsync();
}
