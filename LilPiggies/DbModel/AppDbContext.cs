namespace LilPiggies.DbModel;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
        : base(contextOptions)
    {
    }

    public DbSet<UserDayGame> UserDayGames { get; set; }
}