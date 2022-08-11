using DDDapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace DDDapp.Infrastructure.Persistence;

public class UsersAPIDbContext : DbContext
{
    public UsersAPIDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}