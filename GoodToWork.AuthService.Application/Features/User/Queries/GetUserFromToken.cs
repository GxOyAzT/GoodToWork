using GoodToWork.AuthService.Application.Interfaces.Repositories;
using GoodToWork.AuthService.Application.Interfaces.Token;
using GoodToWork.AuthService.Domain.Entities;
using MediatR;

namespace GoodToWork.AuthService.Application.Features.User.Queries;

public static class GetUserFromToken
{
    public sealed record Query(string Token) : IRequest<UserModel>;

    public sealed class Handler : IRequestHandler<Query, UserModel>
    {
        private readonly ITokenDeserializer _tokenDeserializer;
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;

        public Handler(
            ITokenDeserializer tokenDeserializer,
            IUserRepository userRepository,
            ISessionRepository sessionRepository)
        {
            _tokenDeserializer = tokenDeserializer;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<UserModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var sessionId = _tokenDeserializer.GetSessionIdFromToken(request.Token);

            var userModel = await _sessionRepository.TryGetUserById(Guid.Parse(sessionId), cancellationToken);

            return userModel;
        }
    }
}
