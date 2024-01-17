using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LilPiggies.Pages.Play;

using MediatR;
using Microsoft.AspNetCore.Mvc;

public class Index : PageModel
{
    private readonly IMediator mediator;

    public Guid BoardId { get; set; }
    public int AvailableAttempts { get; set; }
    public bool Completed { get; set; }

    [BindProperty]
    public string Solution { get; set; }

    //temp
    public const string UserId = "123";

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
        await mediator.Send(new Command {Solution = Solution});
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

    private static int attempts = 0;
    private static bool finished = false;
    private const int MaxAttempts = 5;

    public class QueryHandler : IRequestHandler<Query, PlayViewModel>
    {
        private static readonly Guid BoardId = Guid.NewGuid();

        public Task<PlayViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new PlayViewModel
            {
                AvailableAttempts = MaxAttempts - attempts,
                BoardId = BoardId,
                Completed = finished
            });
        }
    }

    public class Command : IRequest
    {
        public string Solution { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command>
    {
        public Task Handle(Command request, CancellationToken cancellationToken)
        {
            attempts++;
            return Task.CompletedTask;
        }
    }
}