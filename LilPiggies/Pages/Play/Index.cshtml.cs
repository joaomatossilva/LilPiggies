using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LilPiggies.Pages.Play;

using DbModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Index : PageModel
{
    private readonly IMediator mediator;

    public Guid BoardId { get; set; }
    public int AvailableAttempts { get; set; }
    public bool Completed { get; set; }

    [BindProperty]
    public string Solution { get; set; }

    //temp
    public const string UserId = "122";

    public Index(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task OnGet()
    {
        var model = await mediator.Send(new Query {UserId = UserId});
        BoardId = model.BoardId;
        AvailableAttempts = model.AvailableAttempts;
        Completed = model.Completed;
    }

    public async Task<IActionResult> OnPost()
    {
        await mediator.Send(new Command {UserId = UserId, Solution = Solution});
        return RedirectToPage("Index");
    }

    public class Query : IRequest<PlayViewModel>
    {
        public string UserId { get; set; }
    }

    public class PlayViewModel
    {
        public Guid BoardId { get; set; }
        public int AvailableAttempts { get; set; }
        public bool Completed { get; set; }
    }

    private const int MaxAttempts = 5;

    public class QueryHandler(AppDbContext dbContext) : IRequestHandler<Query, PlayViewModel>
    {
        private static readonly Guid BoardId = Guid.NewGuid();

        public async Task<PlayViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var day = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            var userDay = await dbContext.UserDayGames.FirstOrDefaultAsync(uDay =>
                uDay.UserId == request.UserId && uDay.Day == day, cancellationToken);

            if (userDay is null)
            {
                userDay = new UserDayGame
                {
                    UserId = request.UserId,
                    Day = day,
                    BoardId = BoardId,
                    Attempts = 0,
                    Completed = false
                };
                dbContext.UserDayGames.Add(userDay);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return new PlayViewModel
            {
                AvailableAttempts = MaxAttempts - userDay.Attempts,
                BoardId = userDay.BoardId,
                Completed = userDay.Completed
            };
        }
    }

    public class Command : IRequest
    {
        public string UserId { get; set; }
        public string Solution { get; set; }
    }

    public class CommandHandler(AppDbContext dbContext) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var day = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            var userDay = await dbContext.UserDayGames.FirstOrDefaultAsync(uDay =>
                uDay.UserId == request.UserId && uDay.Day == day, cancellationToken);

            if (userDay is null)
            {
                return;
            }

            if (userDay.Attempts >= MaxAttempts)
            {
                //if we're hitting this, we're getting abused
                return;
            }

            userDay.Attempts++;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}