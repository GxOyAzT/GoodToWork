using GoodToWork.TasksOrganizer.Application.Builders.Entities.Project;
using GoodToWork.TasksOrganizer.Domain.Enums;
using System;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Builders.Entities.Project;

public class BuildNewProjectEntityTests
{
    [Fact]
    public void BuilderTestA()
    {
        var testedUnit = ProjectEntityBuilder.Create()
                            .WithName("valid_name")
                            .WithDescription("valid_description")
                            .WithCreator(Guid.Empty)
                            .Build();

        Assert.Equal("valid_name", testedUnit.Name);
        Assert.Equal("valid_description", testedUnit.Description);
        Assert.Single(testedUnit.ProjectUsers);
        Assert.Equal(Guid.Empty, testedUnit.ProjectUsers[0].UserId);
        Assert.Equal(UserProjectRoleEnum.Moderator, testedUnit.ProjectUsers[0].Role);
    }
}
