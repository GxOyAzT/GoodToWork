using Dapper;
using GoodToWork.AuthService.Application.Interfaces.Repositories;
using GoodToWork.AuthService.Domain.Entities;
using GoodToWork.AuthService.Infrastructure.Configurations;
using GoodToWork.AuthService.Infrastructure.Exceptions;
using System.Data.SqlClient;

namespace GoodToWork.AuthService.Infrastructure.Persistance.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public SessionRepository(
        DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async Task<SessionModel> AddSessionAsync(SessionModel sessionModel, CancellationToken ct)
    {
        using var connection = new SqlConnection(_databaseConfig.ConnectionString);
        try
        {
            connection.Open();

            await connection.ExecuteAsync(
                new CommandDefinition($"execute AddSession '{sessionModel.Id}', '{sessionModel.UserId}', '{sessionModel.ExpirationDate.ToString("yyyy-MM-dd")}'",
                ct));

            return sessionModel;
        }
        catch(Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }

    public async Task DeactivateAllUserSessionsAsync(Guid userId, CancellationToken ct)
    {
        using var connection = new SqlConnection(_databaseConfig.ConnectionString);
        try
        {
            connection.Open();

            await connection.ExecuteAsync(new CommandDefinition($"execute LogoutFromAllSessions '{userId}'", ct));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }

    public async Task<UserModel> TryGetUserByIdAsync(Guid id, CancellationToken ct)
    {
        using var connection = new SqlConnection(_databaseConfig.ConnectionString);
        try
        {
            connection.Open();

            var userModel = (await connection.QueryAsync<UserModel>(new CommandDefinition($"execute GetUserBySessionId '{id}'", ct))).FirstOrDefault();

            if (userModel == null)
            {
                throw new CannotFindException($"Cannot find session of id {id}");
            }

            return userModel;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }
}
