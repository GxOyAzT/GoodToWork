using GoodToWork.TasksOrganizer.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GoodToWork.TasksOrganizer.Infrastructure.Tests.Mocks.EntitiesGenerator.User;

internal class GenerateUsers
{
    public static List<UserEntity> Empty()
    {
        return new List<UserEntity>();
    }

    public static List<UserEntity> ThreeUsers()
    {
        return new List<UserEntity>()
        {
            new UserEntity()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "User - 1"
            },
            new UserEntity()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Name = "User - 2"
            },
            new UserEntity()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Name = "User - 3"
            }
        };
    }
}
