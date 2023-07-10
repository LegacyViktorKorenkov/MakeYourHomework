using MakeYourHomework.AuthService.Data;
using MakeYourHomework.AuthService.Models;
using MakeYourHomework.AuthService.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MakeYourHomework.AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDataContext _authDataContext;

        public UserRepository(AuthDataContext authDataContext)
        {
            _authDataContext = authDataContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> filter = null)
        {
            IQueryable<User> query = _authDataContext.Set<User>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var users = await query.ToListAsync();

            return users;
        }

        public async Task<User> GetAsync(
            Expression<Func<User, bool>> filter = null, 
            Func<IQueryable<User>, IIncludableQueryable<User, object>> includes = null)
        {
            IQueryable<User> query = _authDataContext.Set<User>();

            if(includes != null)
            {
                query = includes(query);
            }

            User user;

            user = filter != null ? await query.FirstOrDefaultAsync(filter) : await query.FirstOrDefaultAsync();

            return user ?? new User();
        }

        public async Task<bool> UserExistAsync(Expression<Func<User, bool>> filter) 
            => await _authDataContext.Users.AnyAsync(filter);

        public async Task SaveChangesAsync()
        {
            await _authDataContext.SaveChangesAsync();
        }
    }
}
