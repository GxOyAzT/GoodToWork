using GoodToWork.TasksOrganizer.Application.ApiModels.Problem;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Application.Features.TaskFeat.Commands;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Problem;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.ProjectUser;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Access;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Validation;
using GoodToWork.TasksOrganizer.Infrastructure.Features.Problem.Queries;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.Problem.Commands;

public class CreateProblemHandlerTests
{
    [Fact]
    public async Task SenderHasNoAccess()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockedProjectUsers = new Mock<IProjectUserRepository>();
        var mockedCurrentDateTime = new Mock<ICurrentDateTime>();
        var mockedMediator = new Mock<IMediator>();

        mockedAppRepo.Setup(m => m.ProjectUsers).Returns(mockedProjectUsers.Object);

        mockedProjectUsers.Setup(m => m.Find(It.IsAny<Func<ProjectUserEntity, bool>>()))
            .Returns(Task.FromResult((ProjectUserEntity)null));

        await Assert.ThrowsAsync<NoAccessException>(() => new CreateProblemHandler(mockedCurrentDateTime.Object, mockedMediator.Object, mockedAppRepo.Object)
            .Handle(new CreateProblemCommand("valid title", "valid description", Guid.Empty, Guid.Empty, Guid.Empty), new CancellationToken()));

        mockedAppRepo.Verify(m => m.Problems, Times.Never());
    }

    [Fact]
    public async Task IncorrectValidation()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockedProjectUsers = new Mock<IProjectUserRepository>();
        var mockedCurrentDateTime = new Mock<ICurrentDateTime>();
        var mockedMediator = new Mock<IMediator>();

        mockedMediator.Setup(m => m.Send(It.IsAny<ValidateProblemQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new ProblemValidationModel() { Description = "not null" }));

        mockedAppRepo.Setup(m => m.ProjectUsers).Returns(mockedProjectUsers.Object);

        mockedProjectUsers.Setup(m => m.Find(It.IsAny<Func<ProjectUserEntity, bool>>()))
            .Returns(Task.FromResult(new ProjectUserEntity()));

        await Assert.ThrowsAsync<ValidationFailedError>(() => new CreateProblemHandler(mockedCurrentDateTime.Object, mockedMediator.Object, mockedAppRepo.Object)
            .Handle(new CreateProblemCommand("valid title", "valid description", Guid.Empty, Guid.Empty, Guid.Empty), new CancellationToken()));

        mockedAppRepo.Verify(m => m.Problems, Times.Never());
    }

    [Fact]
    public async Task CorrectInput()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockedProjectUsers = new Mock<IProjectUserRepository>();
        var mockedProblems = new Mock<IProblemRepository>();
        var mockedCurrentDateTime = new Mock<ICurrentDateTime>();
        var mockedMediator = new Mock<IMediator>();

        mockedMediator.Setup(m => m.Send(It.IsAny<ValidateProblemQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new ProblemValidationModel()));

        mockedProblems.Setup(m => m.Add(It.IsAny<ProblemEntity>()))
            .Returns(Task.FromResult(new ProblemEntity()));

        mockedAppRepo.Setup(m => m.ProjectUsers).Returns(mockedProjectUsers.Object);
        mockedAppRepo.Setup(m => m.Problems).Returns(mockedProblems.Object);

        mockedProjectUsers.Setup(m => m.Find(It.IsAny<Func<ProjectUserEntity, bool>>()))
            .Returns(Task.FromResult(new ProjectUserEntity()));

        await new CreateProblemHandler(mockedCurrentDateTime.Object, mockedMediator.Object, mockedAppRepo.Object)
            .Handle(new CreateProblemCommand("valid title", "valid description", Guid.Empty, Guid.Empty, Guid.Empty), new CancellationToken());

        mockedAppRepo.Verify(m => m.Problems.Add(It.IsAny<ProblemEntity>()), Times.Once());
    }
}
