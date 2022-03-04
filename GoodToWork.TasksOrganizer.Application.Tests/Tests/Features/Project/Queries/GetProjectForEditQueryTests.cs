using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.Features.Project.Queries;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.User;
using GoodToWork.TasksOrganizer.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.Project.Queries;

public class GetProjectForEditQueryTests
{
    [Fact]
    public async Task ProjectDoNotExists()
    {
        var mockedAppRepository = new Mock<IAppRepository>();
        var mockedProjectsRepo = new Mock<IProjectRepository>();

        mockedProjectsRepo.Setup(pr => pr.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(new List<ProjectEntity>()));

        mockedAppRepository.Setup(ar => ar.Projects).Returns(mockedProjectsRepo.Object);

        var request = new GetProjectForEditQuery(Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.Parse("00000000-0000-0000-0000-000000000001"));

        Assert.ThrowsAsync<CannnotFindException>(() => new GetProjectForEditHandler(mockedAppRepository.Object).Handle(request, new CancellationToken()));
    }

    [Fact]
    public async Task NoAvaliableUsers()
    {
        var mockedAppRepository = new Mock<IAppRepository>();
        var mockedProjectsRepo = new Mock<IProjectRepository>();
        var mockedUsersRepo = new Mock<IUserRepository>();

        mockedProjectsRepo.Setup(pr => pr.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(new List<ProjectEntity>() 
            { 
                new ProjectEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), ProjectUsers = new List<ProjectUserEntity>() } 
            }));

        mockedUsersRepo.Setup(ur => ur.Get())
            .Returns(Task.FromResult(new List<UserEntity>()));

        mockedAppRepository.Setup(ar => ar.Projects).Returns(mockedProjectsRepo.Object);
        mockedAppRepository.Setup(ar => ar.Users).Returns(mockedUsersRepo.Object);

        var request = new GetProjectForEditQuery(Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.Parse("00000000-0000-0000-0000-000000000001"));

        var returnModel = await new GetProjectForEditHandler(mockedAppRepository.Object).Handle(request, new CancellationToken());

        Assert.Empty(returnModel.AvaliableUsers);
        Assert.Empty(returnModel.AddedUsers);
    }

    [Fact]
    public async Task NoUserAdded()
    {
        var mockedAppRepository = new Mock<IAppRepository>();
        var mockedProjectsRepo = new Mock<IProjectRepository>();
        var mockedUsersRepo = new Mock<IUserRepository>();

        mockedProjectsRepo.Setup(pr => pr.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(new List<ProjectEntity>()
            {
                new ProjectEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), ProjectUsers = new List<ProjectUserEntity>() }
            }));

        mockedUsersRepo.Setup(ur => ur.Get())
            .Returns(Task.FromResult(new List<UserEntity>() 
            { 
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000002") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000003") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000004") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000005") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000006") },
            }));

        mockedAppRepository.Setup(ar => ar.Projects).Returns(mockedProjectsRepo.Object);
        mockedAppRepository.Setup(ar => ar.Users).Returns(mockedUsersRepo.Object);

        var request = new GetProjectForEditQuery(Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.Parse("00000000-0000-0000-0000-000000000001"));

        var returnModel = await new GetProjectForEditHandler(mockedAppRepository.Object).Handle(request, new CancellationToken());

        Assert.Equal(6, returnModel.AvaliableUsers.Count());
        Assert.Empty(returnModel.AddedUsers);
    }

    [Fact]
    public async Task Ok()
    {
        var mockedAppRepository = new Mock<IAppRepository>();
        var mockedProjectsRepo = new Mock<IProjectRepository>();
        var mockedUsersRepo = new Mock<IUserRepository>();

        mockedProjectsRepo.Setup(pr => pr.GetWithUsers(It.IsAny<Func<ProjectEntity, bool>>()))
            .Returns(Task.FromResult(new List<ProjectEntity>()
            {
                new ProjectEntity() 
                { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), ProjectUsers = new List<ProjectUserEntity>() { 
                    new ProjectUserEntity() { UserId = Guid.Parse("00000000-0000-0000-0000-000000000002"), User = 
                        new UserEntity()
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            Name = "User 2"
                        }
                    },
                    new ProjectUserEntity() { UserId = Guid.Parse("00000000-0000-0000-0000-000000000004"), User = 
                        new UserEntity()
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            Name = "User 2"
                        } }
                } }
            }));

        mockedUsersRepo.Setup(ur => ur.Get())
            .Returns(Task.FromResult(new List<UserEntity>()
            {
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000002") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000003") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000004") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000005") },
                new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000006") },
            }));

        mockedAppRepository.Setup(ar => ar.Projects).Returns(mockedProjectsRepo.Object);
        mockedAppRepository.Setup(ar => ar.Users).Returns(mockedUsersRepo.Object);

        var request = new GetProjectForEditQuery(Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.Parse("00000000-0000-0000-0000-000000000001"));

        var returnModel = await new GetProjectForEditHandler(mockedAppRepository.Object).Handle(request, new CancellationToken());

        Assert.Equal(4, returnModel.AvaliableUsers.Count());
        Assert.Equal(2, returnModel.AddedUsers.Count());
    }
}
