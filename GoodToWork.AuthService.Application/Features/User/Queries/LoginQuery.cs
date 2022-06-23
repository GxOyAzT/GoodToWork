using GoodToWork.AuthService.Application.ApiModels.User;
using GoodToWork.AuthService.Application.Interfaces.Repositories;
using GoodToWork.AuthService.Application.Interfaces.Token;
using GoodToWork.AuthService.Domain.Entities;
using MediatR;

namespace GoodToWork.AuthService.Application.Features.User.Queries;

public static class LoginQuery
{
    public sealed record Query(string User, string Password) : IRequest<TokenModel>;

    public sealed class Handler : IRequestHandler<Query, TokenModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public Handler(
            IUserRepository userRepository,
            ISessionRepository sessionRepository,
            ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<TokenModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.TryFindUserByPassword(request.User, request.Password, cancellationToken);

            var session = new SessionModel()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ExpirationDate = DateTime.Now.AddDays(7)
            };

            await _sessionRepository.AddSessionAsync(session, cancellationToken);

            return new TokenModel() 
            { 
                Email = user.Email, 
                Token = _tokenGenerator.GenerateToken(user, session) 
            };
        }
    }
}

