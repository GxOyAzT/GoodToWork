using GoodToWork.TasksOrganizer.Domain.Entities;
using System.Collections.Generic;
using System;

namespace GoodToWork.TasksOrganizer.Infrastructure.Tests.Mocks.EntitiesGenerator.Project;

internal class GenerateProjects
{
    public static List<ProjectEntity> Empty()
    {
        return new List<ProjectEntity>();
    }

    public static List<ProjectEntity> TwoProjectsWithUserProject()
    {
        return new List<ProjectEntity>()
        {
            new ProjectEntity()
            {
                Id = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                Name = "Project_1",
                ProjectUsers = new List<ProjectUserEntity>()
                {
                    new ProjectUserEntity()
                    {
                        UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                        Role = Domain.Enums.UserProjectRoleEnum.Moderator
                    },
                    new ProjectUserEntity()
                    {
                        UserId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    }
                }
            },
            new ProjectEntity()
            {
                Id = Guid.Parse("00000000-0000-0000-0002-000000000000"),
                Name = "Project_1",
                ProjectUsers = new List<ProjectUserEntity>()
                {
                    new ProjectUserEntity()
                    {
                        UserId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                    },
                }
            }
        };
    }
}
