namespace LilPiggies.DbModel;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(UserId), nameof(Day))]
public class UserDayGame
{
    public string UserId { get; set; }
    public DateOnly Day { get; set; }
    public Guid BoardId { get; set; }
    public int Attempts { get; set; }
    public bool Completed { get; set; }
}