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

        var testedUnit = ProjectEntityBuilder.Create(mockedCurrentDateTime.Object)
                            .WithName("valid_name")
                            .WithDescription("valid_description")
                            .WithCreator(Guid.Empty)
                            .Build();

        Assert.Equal("valid_name", testedUnit.Name);
        Assert.Equal("valid_description", testedUnit.Description);
        Assert.Equal(new DateTime(2022, 1, 15), testedUnit.Created);
        Assert.Single(testedUnit.ProjectUsers);
        Assert.Equal(Guid.Empty, testedUnit.ProjectUsers[0].UserId);
        Assert.Equal(UserProjectRoleEnum.Moderator, testedUnit.ProjectUsers[0].Role);
    }
}
