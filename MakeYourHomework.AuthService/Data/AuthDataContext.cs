using MakeYourHomework.AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MakeYourHomework.AuthService.Data;

public class AuthDataContext : IdentityDbContext<IdentityUser>
{
    public DbSet<User> Users { get; set; }

    public AuthDataContext(DbContextOptions<AuthDataContext> opt) : base(opt)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
