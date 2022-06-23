using GoodToWork.AuthService.Application.Interfaces.Repositories;
using MediatR;

namespace GoodToWork.AuthService.Application.Features.User.Commands;

public static class LogoutAllSessions
{
    public sealed record Command(Guid SenderId) : IRequest<Unit>;

    public sealed class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ISessionRepository _sessionRepo;

        public Handler(
            ISessionRepository sessionRepo)
        {
            _sessionRepo = sessionRepo;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            await _sessionRepo.DeactivateAllUserSessionsAsync(request.SenderId, cancellationToken);

            return Unit.Value;
        }
    }
}
