using GoodToWork.Shared.Common.Domain.Exceptions.Validation;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Application.Features.Project.Commands;
using GoodToWork.TasksOrganizer.Application.Features.Project.Queries;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.Project.Commands;

public class CreateProjectHandlerTests
{
    [Fact]
    public async Task Check_Validation_ThrowsException()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockedMediator = new Mock<IMediator>();
        var mockedCurrentDateTime = new Mock<ICurrentDateTime>();

        mockedCurrentDateTime.Setup(cdt => cdt.CurrentDateTime).Returns(new DateTime(2022, 1, 15));

        mockedMediator.Setup(m => m.Send(It.IsAny<ValidateProjectInputQuery>(), It.IsAny<CancellationToken>()))
            .Throws(new ValidationFailedException($"Passed object is invalid.", System.Net.HttpStatusCode.BadRequest, new object()));

        var input = new CreateProjectCommand("", "", Guid.Empty);

        var testedUnit = new CreateProjectHandler(mockedMediator.Object, mockedAppRepo.Object, mockedCurrentDateTime.Object);

        await Assert.ThrowsAsync<ValidationFailedException>(() => testedUnit.Handle(input, new CancellationToken()));

        mockedAppRepo.Verify(v => v.SaveChangesAsync(), Times.Never());
    }

    [Fact]
    public async Task Check_Validation_Ok()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockedProjectRepo = new Mock<IProjectRepository>();
        var mockedCurrentDateTime = new Mock<ICurrentDateTime>();

        mockedCurrentDateTime.Setup(cdt => cdt.CurrentDateTime).Returns(new DateTime(2022, 1, 15));

        mockedAppRepo.Setup(m => m.Projects).Returns(mockedProjectRepo.Object);
        
        var mockedMediator = new Mock<IMediator>();

        mockedMediator.Setup(m => m.Send(It.IsAny<ValidateProjectInputQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        var input = new CreateProjectCommand("valid_name", "valid_description", Guid.Empty);

        var testedUnit = new CreateProjectHandler(mockedMediator.Object, mockedAppRepo.Object, mockedCurrentDateTime.Object);

        await testedUnit.Handle(input, new CancellationToken());

        mockedProjectRepo.Verify(v => v.Add(It.Is<ProjectEntity>(x => x.Name == "valid_name" && x.Description == "valid_description" && x.Created == new DateTime(2022, 1, 15))), Times.Once);
        mockedAppRepo.Verify(v => v.SaveChangesAsync(), Times.Once());
    }
}
