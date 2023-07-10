using MakeYourHomework.HomeworkService.Models;
using Microsoft.EntityFrameworkCore;

namespace MakeYourHomework.HomeworkService.Data;

public class HomeworkDataContext : DbContext
{
    public DbSet<User> Users{ get; set; }
    public DbSet<Homework> Homeworks { get; set; }
    public DbSet<Assigment> Assigments { get; set; }

    public HomeworkDataContext(DbContextOptions<HomeworkDataContext> opt) : base(opt)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
    }
}
