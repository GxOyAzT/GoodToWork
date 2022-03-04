using GoodToWork.TasksOrganizer.Application.ApiModels.Problem;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.ApiModels.Problem;

public class ProblemDetailModelTests
{
    [Fact]
    public void TestA()
    {
        var mockedCurrentTime = new Mock<ICurrentDateTime>();

        mockedCurrentTime.Setup(cdt => cdt.CurrentDateTime).Returns(new DateTime(2022, 1, 1, 10, 35, 40));

        var problem = new ProblemEntity()
        {
            Id = System.Guid.NewGuid(),
            Title = "_title_",
            Created = new DateTime(2022,2,3,10,50,23),
            Creator = new UserEntity()
            {
                Name = "_user2_"
            },
            Statuses = new List<StatusEntity>() 
            {
                new StatusEntity()
                {
                    Status = ProblemStatusEnum.ToFix,
                    Updator = new UserEntity()
                    {
                        Name = "_user1_",
                    },
                    Updated = new DateTime(2022,2,1)
                },
                new StatusEntity()
                {
                    Status = ProblemStatusEnum.Created,
                    Updator = new UserEntity()
                    {
                        Name = "_user2_"
                    },
                    Updated = new DateTime(2022,1,25)
                }
            }
        };

        var problemModel = new ProblemDetailModel(problem, mockedCurrentTime.Object);

        Assert.Equal(problem.Id, problemModel.Id);
        Assert.Equal(problem.Title, problemModel.Title);
        Assert.Equal(problem.Creator.Name, problemModel.CreatorName);
        Assert.Equal("10:50 03-02-2022", problemModel.Created);
        Assert.Equal(ProblemStatusEnum.ToFix, problemModel.ProblemStatus);
    }
}
