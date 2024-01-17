namespace LilPiggies.DbModel;

using Microsoft.EntityFrameworkCore;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<UserDayGame> UserDayGames { get; set; }
}