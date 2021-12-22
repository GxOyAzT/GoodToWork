using GoodToWork.TasksOrganizer.Application.Features.User.Commands;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Features.User.Commands;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using GoodToWork.TasksOrganizer.Infrastructure.Tests.Mocks.DbContext;
using GoodToWork.TasksOrganizer.Infrastructure.Tests.Mocks.EntitiesGenerator.User;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Infrastructure.Tests.Tests.Features.User.Commands;

public class UpdateUserHandlerTests
{
    [Fact]
    public async Task TestWithNoUserOfIdExists()
    {
        var _mockedDbContext = new Mock<AppDbContext>();

        var _mockedDbSet = MockDbContext.CreateDbSetMock(GenerateUsers.ThreeUsers());

        _mockedDbContext.Setup(m => m.Users)
            .Returns(_mockedDbSet.Object);

        var input = new UpdateUserCommand(Guid.Parse("00000000-0000-0000-0000-000000000004"), "Name - 4");

        var testedUnit = new UpdateUserHandler(_mockedDbContext.Object);

        await testedUnit.Handle(input, new CancellationToken());

        _mockedDbContext.Verify(e => e.Users.AddAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockedDbContext.Verify(e => e.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockedDbContext.Verify(e => e.Users.Update(It.IsAny<UserEntity>()), Times.Never);
        _mockedDbContext.Verify(e => e.Update(It.IsAny<UserEntity>()), Times.Never);
        _mockedDbContext.Verify(e => e.AddAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task TestWithExistingUser()
    {
        var _mockedDbContext = new Mock<AppDbContext>();

        var _mockedDbSet = MockDbContext.CreateDbSetMock(GenerateUsers.ThreeUsers());

        _mockedDbContext.Setup(m => m.Users)
            .Returns(_mockedDbSet.Object);

        var input = new UpdateUserCommand(Guid.Parse("00000000-0000-0000-0000-000000000003"), "Name - 4");

        var testedUnit = new UpdateUserHandler(_mockedDbContext.Object);

        await testedUnit.Handle(input, new CancellationToken());

        _mockedDbContext.Verify(e => e.Users.AddAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockedDbContext.Verify(e => e.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockedDbContext.Verify(e => e.Users.Update(It.IsAny<UserEntity>()), Times.Once);
        _mockedDbContext.Verify(e => e.Update(It.IsAny<UserEntity>()), Times.Never);
        _mockedDbContext.Verify(e => e.AddAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
