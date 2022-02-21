using GoodToWork.TasksOrganizer.Application.ApiModels.Project;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.ApiModels.Project;

public class ProjectDetailModelTetss
{
    [Fact]
    public void TestA()
    {
        var projectEntity = new ProjectEntity()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Name = "Project_1",
            Problems = new List<ProblemEntity>()
            {
                new ProblemEntity()
                {
                    Id = Guid.Parse("00000000-0000-0001-0000-000000000000"),
                    Title = "Problem_1",
                    Statuses = new List<StatusEntity>()
                    {
                        new StatusEntity()
                        {
                            Id = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                            Updated = new DateTime(2022,1,1),
                            Status = ProblemStatusEnum.Created
                        },
                        new StatusEntity()
                        {
                            Id = Guid.Parse("00000000-0000-0000-0002-000000000000"),
                            Updated = new DateTime(2022,1,5),
                            Status = ProblemStatusEnum.InProgress
                        },
                        new StatusEntity()
                        {
                            Id = Guid.Parse("00000000-0000-0000-0003-000000000000"),
                            Updated = new DateTime(2022,1,3),
                            Status = ProblemStatusEnum.ToFix
                        }
                    },
                    Performer = new UserEntity()
                    {
                        Name = "User_1"
                    }
                },
                new ProblemEntity()
                {
                    Id = Guid.Parse("00000000-0000-0002-0000-000000000000"),
                    Title = "Problem_2",
                    Statuses = new List<StatusEntity>()
                    {
                        new StatusEntity()
                        {
                            Id = Guid.Parse("00000000-0000-0000-0004-000000000000"),
                            Updated = new DateTime(2022,1,12),
                            Status = ProblemStatusEnum.Created
                        },
                        new StatusEntity()
                        {
                            Id = Guid.Parse("00000000-0000-0000-0005-000000000000"),
                            Updated = new DateTime(2022,1,5),
                            Status = ProblemStatusEnum.InProgress
                        },
                        new StatusEntity()
                        {
                            Id = Guid.Parse("00000000-0000-0000-0006-000000000000"),
                            Updated = new DateTime(2022,1,3),
                            Status = ProblemStatusEnum.ToFix
                        }
                    },
                    Performer = new UserEntity()
                    {
                        Name = "User_2"
                    }
                }
            }
        };

        var projectDetailModel = new ProjectDetailModel(projectEntity);

        Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000001"), projectDetailModel.Id);

        Assert.Equal(ProblemStatusEnum.InProgress, projectDetailModel.Problems.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0001-0000-000000000000")).ProblemStatus);
        Assert.Equal(ProblemStatusEnum.Created, projectDetailModel.Problems.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0002-0000-000000000000")).ProblemStatus);
    }
}
