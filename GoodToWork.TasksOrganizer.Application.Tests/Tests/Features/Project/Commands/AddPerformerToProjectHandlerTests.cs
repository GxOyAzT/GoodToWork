using GoodToWork.Shared.Common.Domain.Exceptions.Access;
using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.Features.Project.Commands;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.User;
using GoodToWork.TasksOrganizer.Application.Tests.Mocks.EntitiesGenerator.Project;
using GoodToWork.TasksOrganizer.Application.Tests.Mocks.EntitiesGenerator.User;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Shared;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.Project.Commands;

public class AddPerformerToProjectHandlerTests
{
    private Mock<IAppRepository> mockedAppRepository;
    private Mock<IProjectRepository> mockedProjectRepository;
    private Mock<IUserRepository> mockedUserRepository;

    public AddPerformerToProjectHandlerTests()
    {
        mockedAppRepository = new Mock<IAppRepository>();
        mockedProjectRepository = new Mock<IProjectRepository>();
        mockedUserRepository = new Mock<IUserRepository>();

        mockedAppRepository.Setup(m => m.Projects)
            .Returns(mockedProjectRepository.Object);

        mockedAppRepository.Setup(m => m.Users)
            .Returns(mockedUserRepository.Object);
    }

    [Fact]
    public async Task ProjectOfIdDoestNotExists()
    {
        mockedProjectRepository.Setup(m => m.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(new List<ProjectEntity>()));

        var request = new AddPerformerToProjectCommand(
            Guid.Parse("00000000-0000-0000-0011-000000000000"),
            Guid.Empty,
            Guid.Empty);

        var testedUnit = new AddPerformerToProjectHandler(mockedAppRepository.Object);

        await Assert.ThrowsAsync<CannnotFindException>(() => testedUnit.Handle(request, new CancellationToken()));

        mockedAppRepository.Verify(v => v.Projects.Add(It.IsAny<ProjectEntity>()), Times.Never);
        mockedAppRepository.Verify(v => v.SaveChanges(), Times.Never);
    }

    [Fact]
    public async Task SenderHasNoAccess()
    {
        mockedProjectRepository.Setup(m => m.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(GenerateProjects.TwoProjectsWithUserProject()));

        var request = new AddPerformerToProjectCommand(
            Guid.Parse("00000000-0000-0000-0000-000000000111"),
            Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Guid.Parse("00000000-0000-0000-0000-000000000002"));

        var testedUnit = new AddPerformerToProjectHandler(mockedAppRepository.Object);

        await Assert.ThrowsAsync<NoAccessException>(() => testedUnit.Handle(request, new CancellationToken()));

        mockedAppRepository.Verify(v => v.Projects.Add(It.IsAny<ProjectEntity>()), Times.Never);
        mockedAppRepository.Verify(v => v.SaveChanges(), Times.Never);
    }

    [Fact]
    public async Task PerformerIsAlreadyAttatchedToProject()
    {
        mockedProjectRepository.Setup(m => m.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(GenerateProjects.TwoProjectsWithUserProject()));

        var request = new AddPerformerToProjectCommand(
            Guid.Parse("00000000-0000-0000-0000-000000000111"),
            Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Guid.Parse("00000000-0000-0000-0000-000000000001"));

        var testedUnit = new AddPerformerToProjectHandler(mockedAppRepository.Object);

        await Assert.ThrowsAsync<DomainException>(() => testedUnit.Handle(request, new CancellationToken()));

        mockedAppRepository.Verify(v => v.Projects.Add(It.IsAny<ProjectEntity>()), Times.Never);
        mockedAppRepository.Verify(v => v.SaveChanges(), Times.Never);
    }

    [Fact]
    public async Task CannotFindUser()
    {
        mockedProjectRepository.Setup(m => m.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(GenerateProjects.TwoProjectsWithUserProject()));

        mockedUserRepository.Setup(m => m.Find(It.IsAny<Func<UserEntity, bool>>()))
            .Returns(Task.FromResult((UserEntity)null));

        var request = new AddPerformerToProjectCommand(
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Guid.Parse("00000000-0000-0000-0000-000000000033"),
            Guid.Parse("00000000-0000-0000-0000-000000000001"));

        var testedUnit = new AddPerformerToProjectHandler(mockedAppRepository.Object);

        await Assert.ThrowsAsync<DomainException>(() => testedUnit.Handle(request, new CancellationToken()));

        mockedAppRepository.Verify(v => v.Projects.Add(It.IsAny<ProjectEntity>()), Times.Never);
        mockedAppRepository.Verify(v => v.SaveChanges(), Times.Never);
    }

    [Fact]
    public async Task CorrectRequestHandle()
    {
        mockedProjectRepository.Setup(m => m.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(GenerateProjects.TwoProjectsWithUserProject()));

        mockedUserRepository.Setup(m => m.Find(It.IsAny<Func<UserEntity, bool>>()))
            .Returns(Task.FromResult(GenerateUsers.ThreeUsers().FirstOrDefault()));

        var request = new AddPerformerToProjectCommand(
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Guid.Parse("00000000-0000-0000-0000-000000000033"),
            Guid.Parse("00000000-0000-0000-0000-000000000001"));

        var testedUnit = new AddPerformerToProjectHandler(mockedAppRepository.Object);

        await testedUnit.Handle(request, new CancellationToken());

        mockedAppRepository.Verify(v => v.Projects.Update(It.Is<ProjectEntity>(input => 
            input.Name == "Project_1" &&
            input.Id == Guid.Parse("00000000-0000-0000-0001-000000000000") &&
            input.ProjectUsers.FirstOrDefault(pu => pu.UserId == Guid.Parse("00000000-0000-0000-0000-000000000033")) != null
        )), Times.Once);
        mockedAppRepository.Verify(v => v.SaveChanges(), Times.Once);
    }
}
