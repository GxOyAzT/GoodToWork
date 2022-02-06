using GoodToWork.TasksOrganizer.Application.Builders.Entities.Problem;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Domain.Enums;
using Moq;
using System;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Builders.Entities.Problem;

public class ProblemEntityBuilderTests
{
    [Fact]
    public void BuildProblemEntity()
    {
        var currentDateTimeMock = new Mock<ICurrentDateTime>();
        currentDateTimeMock.Setup(m => m.CurrentDateTime)
            .Returns(new DateTime(2021,5,11));

        var problemEntity = ProblemEntityBuilder.Create(currentDateTimeMock.Object)
            .WithTitle("valid title")
            .WithDescription("valid description")
            .WithProjectId(Guid.Parse("00000000-0000-0000-0000-000000000001"))
            .WithPerfomerId(Guid.Parse("00000000-0000-0000-0000-000000000002"))
            .WithCreatorId(Guid.Parse("00000000-0000-0000-0000-000000000003"))
            .Build();

        Assert.Equal("valid title", problemEntity.Title);
        Assert.Equal("valid description", problemEntity.Description);
        Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000001"), problemEntity.ProjectId);
        Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000002"), problemEntity.PerformerId);
        Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000003"), problemEntity.CreatorId);
        Assert.Single(problemEntity.Statuses);
        Assert.Equal(ProblemStatusEnum.Created, problemEntity.Statuses[0].Status);
        Assert.Equal(new DateTime(2021, 5, 11), problemEntity.Statuses[0].Updated);
        Assert.Equal(new DateTime(2021, 5, 11), problemEntity.Created);
    }
}
