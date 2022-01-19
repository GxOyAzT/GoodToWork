using GoodToWork.NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Mocked.UserCollection;

internal class MockUserEntities
{
    public static List<UserEntity> Empty => new List<UserEntity>();

    public static List<UserEntity> Single => new List<UserEntity>()
    {
        new UserEntity()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Email = "user1@test.com",
            UserName = "user 1"
        }
    };

    public static List<UserEntity> Three => new List<UserEntity>()
    {
        new UserEntity()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Email = "user1@test.com",
            UserName = "user 1"
        },
        new UserEntity()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Email = "user2@test.com",
            UserName = "user 2"
        },
        new UserEntity()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
            Email = "user3@test.com",
            UserName = "user 3"
        }
    };
}
