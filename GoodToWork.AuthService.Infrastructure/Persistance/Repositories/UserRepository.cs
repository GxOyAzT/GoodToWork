using Dapper;
using GoodToWork.AuthService.Application.Interfaces.Repositories;
using GoodToWork.AuthService.Domain.Entities;
using GoodToWork.AuthService.Infrastructure.Configurations;
using GoodToWork.AuthService.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace GoodToWork.AuthService.Infrastructure.Persistance.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly DatabaseConfig _databaseConfig;
    private readonly ILogger<UserRepository> logger;

    public UserRepository(
        DatabaseConfig databaseConfig,
        ILogger<UserRepository> logger)
    {
        _databaseConfig = databaseConfig;
        this.logger = logger;
    }

    public async Task<UserModel> TryFindUserById(Guid id, CancellationToken ct)
    {
        using var connection = new SqlConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var userModel = (await connection.QueryAsync<UserModel>(new CommandDefinition($"execute FindUserById '{id}'", ct))).FirstOrDefault();

        connection.Close();

        if (userModel == null)
        {
            throw new CannotFindException($"Cannot find user of id {id}.");
        }

        return userModel;
    }

    public async Task<UserModel> TryFindUserByPassword(string user, string password, CancellationToken ct)
    {
        using var connection = new SqlConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var userModel = (await connection.QueryAsync<UserModel>(new CommandDefinition($"execute FindUserByPassword {user}, {password}", ct))).FirstOrDefault();

        connection.Close();

        if (userModel == null)
        {
            throw new CannotFindException("Cannot find user of username and password.");
        }

        return userModel;
    }
}
