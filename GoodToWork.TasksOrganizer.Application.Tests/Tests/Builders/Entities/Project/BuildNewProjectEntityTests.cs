using GoodToWork.TasksOrganizer.Application.Builders.Entities.Project;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Domain.Enums;
using Moq;
using System;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Builders.Entities.Project;

public class BuildNewProjectEntityTests
{
    [Fact]
    public void BuilderTestA()
    {
        var mockedCurrentDateTime = new Mock<ICurrentDateTime>();

        mockedCurrentDateTime.Setup(cdt => cdt.CurrentDateTime).Returns(new DateTime(2022, 1, 15));

        var newProject = ProjectEntityBuilder.Create(mockedCurrentDateTime.Object)
                            .WithName("valid_name")
                            .WithDescription("valid_description")
                            .WithCreator(Guid.Empty)
                            .Build();

        Assert.Equal("valid_name", newProject.Name);
        Assert.Equal("valid_description", newProject.Description);
        Assert.Equal(new DateTime(2022, 1, 15), newProject.Created);
        Assert.Single(newProject.ProjectUsers);
        Assert.Equal(Guid.Empty, newProject.ProjectUsers[0].UserId);
        Assert.Equal(UserProjectRoleEnum.Moderator, newProject.ProjectUsers[0].Role);
        Assert.True(newProject.IsActive);
    }
}
