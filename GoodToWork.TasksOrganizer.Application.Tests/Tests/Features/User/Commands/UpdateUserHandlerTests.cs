using GoodToWork.TasksOrganizer.Application.Features.User.Commands;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.User;
using GoodToWork.TasksOrganizer.Application.Tests.Mocks.EntitiesGenerator.User;
using GoodToWork.TasksOrganizer.Domain.Entities;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.User.Commands;

public class UpdateUserHandlerTests
{
    [Fact]
    public async Task TestWithNoUserOfIdExists()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockUserRepo = new Mock<IUserRepository>();

        mockUserRepo.Setup(x => x.Find(It.IsAny<Func<UserEntity, bool>>()))
            .Returns(Task.FromResult((UserEntity) null));

        mockedAppRepo.Setup(x => x.Users).Returns(mockUserRepo.Object);

        var input = new UpdateUserCommand(Guid.Parse("00000000-0000-0000-0000-000000000004"), "Name - 4");

        var testedUnit = new UpdateUserHandler(mockedAppRepo.Object);

        await testedUnit.Handle(input, new CancellationToken());

        mockUserRepo.Verify(e => e.Add(It.IsAny<UserEntity>()), Times.Once);
        mockUserRepo.Verify(e => e.Update(It.IsAny<UserEntity>()), Times.Never);
    }

    [Fact]
    public async Task TestWithExistingUser()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockUserRepo = new Mock<IUserRepository>();

        mockUserRepo.Setup(x => x.Find(It.IsAny<Func<UserEntity, bool>>()))
            .Returns(Task.FromResult(GenerateUsers.ThreeUsers().FirstOrDefault()));

        mockedAppRepo.Setup(x => x.Users).Returns(mockUserRepo.Object);

        var input = new UpdateUserCommand(Guid.Parse("00000000-0000-0000-0000-000000000003"), "Name - 4");

        var testedUnit = new UpdateUserHandler(mockedAppRepo.Object);

        await testedUnit.Handle(input, new CancellationToken());

        mockUserRepo.Verify(e => e.Add(It.IsAny<UserEntity>()), Times.Never);
        mockUserRepo.Verify(e => e.Update(It.IsAny<UserEntity>()), Times.Once);
    }
}
