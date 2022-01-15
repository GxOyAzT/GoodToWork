using GoodToWork.TasksOrganizer.Application.Features.Project.Queries;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.Project.Queries;

public class GetSenderProjectsHandlerTests
{
    [Fact]
    public async Task SenderHasProjects()
    {
        var mockedAppRepository = new Mock<IAppRepository>();
        var mockedProjectsRepo = new Mock<IProjectRepository>();

        mockedAppRepository.Setup(ar => ar.Projects).Returns(mockedProjectsRepo.Object);

        mockedProjectsRepo.Setup(pr => pr.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(new List<ProjectEntity>() { new ProjectEntity() }));

        var testedUnit = new GetSenderProjectsHandler(mockedAppRepository.Object);

        var result = await testedUnit.Handle(new GetSenderProjectsQuery(Guid.Empty), new CancellationToken());

        Assert.Single(result);
    }

    [Fact]
    public async Task SenderHasNoProject()
    {
        var mockedAppRepository = new Mock<IAppRepository>();
        var mockedProjectsRepo = new Mock<IProjectRepository>();

        mockedAppRepository.Setup(ar => ar.Projects).Returns(mockedProjectsRepo.Object);

        mockedProjectsRepo.Setup(pr => pr.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(new List<ProjectEntity>()));

        var testedUnit = new GetSenderProjectsHandler(mockedAppRepository.Object);

        await Assert.ThrowsAsync<CannnotFindException>(() => testedUnit.Handle(new GetSenderProjectsQuery(Guid.Empty), new CancellationToken()));
    }
}
