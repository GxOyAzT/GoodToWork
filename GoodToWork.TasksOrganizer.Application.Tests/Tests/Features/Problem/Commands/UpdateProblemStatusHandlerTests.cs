using GoodToWork.Shared.Common.Domain.Exceptions.Access;
using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.Features.Problem.Commands;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Problem;
using GoodToWork.TasksOrganizer.Application.Tests.Mocks.CurrentDateTime;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.Problem.Commands;

public class UpdateProblemStatusHandlerTests
{
    Mock<IAppRepository> mockedAppRepository;
    Mock<IProblemRepository> mockedProblemRepository;

    public UpdateProblemStatusHandlerTests()
    {
        mockedAppRepository = new Mock<IAppRepository>();
        mockedProblemRepository = new Mock<IProblemRepository>();

        mockedAppRepository.Setup(m => m.Problems).Returns(mockedProblemRepository.Object);

        mockedProblemRepository.Setup(m => m.FindProblemWithStatuses(It.IsAny<Func<ProblemEntity, bool>>()))
            .Returns(Task.FromResult(new ProblemEntity()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatorId = Guid.Parse("00000000-0000-0001-0000-000000000000"),
                PerformerId = Guid.Parse("00000000-0000-0002-0000-000000000000"),
                Statuses = new System.Collections.Generic.List<StatusEntity>()
                {
                    new StatusEntity()
                    {
                        Id = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                        Status = ProblemStatusEnum.Created,
                        Updated = new DateTime(2021,3,1)
                    },
                    new StatusEntity()
                    {
                        Id = Guid.Parse("00000000-0000-0000-0002-000000000000"),
                        Status = ProblemStatusEnum.ToFix,
                        Updated = new DateTime(2021,3,2)
                    }
                }
            }));
    }

    [Fact]
    public async Task CorrectUpdate()
    {
        var updateProblemStatusCommand = new UpdateProblemStatusCommand(
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            ProblemStatusEnum.InProgress,
            Guid.Parse("00000000-0000-0002-0000-000000000000"));

        await new UpdateProblemStatusHandler(
            CurrentDateTimeMock.MockOne().Object,
            mockedAppRepository.Object)
            .Handle(updateProblemStatusCommand, new CancellationToken());

        mockedAppRepository.Verify(m => m.Problems.Update(
            It.Is<ProblemEntity>(e => e.Statuses[2].Status == ProblemStatusEnum.InProgress && 
            e.Statuses[2].Updated == new DateTime(2021, 5, 12, 11, 15, 30))), 
                Times.Once);
        mockedAppRepository.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    [Fact]
    public async Task ProblemDoNotExists()
    {
        var updateProblemStatusCommand = new UpdateProblemStatusCommand(
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            ProblemStatusEnum.InProgress,
            Guid.Parse("00000000-0000-0001-0000-000000000000"));

        mockedProblemRepository.Setup(m => m.FindProblemWithStatuses(It.IsAny<Func<ProblemEntity, bool>>()))
            .Returns(Task.FromResult((ProblemEntity)null));

        await Assert.ThrowsAsync<CannnotFindException>(() => new UpdateProblemStatusHandler(
            CurrentDateTimeMock.MockOne().Object,
            mockedAppRepository.Object)
            .Handle(updateProblemStatusCommand, new CancellationToken()));

        mockedAppRepository.Verify(m => m.Problems.Update(
            It.IsAny<ProblemEntity>()), Times.Never);
        mockedAppRepository.Verify(m => m.SaveChangesAsync(), Times.Never());
    }

    [Fact]
    public async Task SenderIdHasNoAccess()
    {
        var updateProblemStatusCommand = new UpdateProblemStatusCommand(
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            ProblemStatusEnum.InProgress,
            Guid.Parse("00000000-0000-0003-0000-000000000000"));

        await Assert.ThrowsAsync<NoAccessException>(() => new UpdateProblemStatusHandler(
            CurrentDateTimeMock.MockOne().Object,
            mockedAppRepository.Object)
            .Handle(updateProblemStatusCommand, new CancellationToken()));

        mockedAppRepository.Verify(m => m.Problems.Update(
            It.IsAny<ProblemEntity>()), Times.Never);
        mockedAppRepository.Verify(m => m.SaveChangesAsync(), Times.Never());
    }

    [Fact]
    public async Task CreatorRequestPerformerStatus()
    {
        var updateProblemStatusCommand = new UpdateProblemStatusCommand(
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            ProblemStatusEnum.InProgress,
            Guid.Parse("00000000-0000-0001-0000-000000000000"));

        await Assert.ThrowsAsync<NoAccessException>(() => new UpdateProblemStatusHandler(
            CurrentDateTimeMock.MockOne().Object,
            mockedAppRepository.Object)
            .Handle(updateProblemStatusCommand, new CancellationToken()));

        mockedAppRepository.Verify(m => m.Problems.Update(
            It.IsAny<ProblemEntity>()), Times.Never);
        mockedAppRepository.Verify(m => m.SaveChangesAsync(), Times.Never());
    }

    [Fact]
    public async Task PerformerRequestCreatorStatus()
    {
        var updateProblemStatusCommand = new UpdateProblemStatusCommand(
            Guid.Parse("00000000-0000-0000-0000-000000000001"),
            ProblemStatusEnum.ToFix,
            Guid.Parse("00000000-0000-0002-0000-000000000000"));

        await Assert.ThrowsAsync<NoAccessException>(() => new UpdateProblemStatusHandler(
            CurrentDateTimeMock.MockOne().Object,
            mockedAppRepository.Object)
            .Handle(updateProblemStatusCommand, new CancellationToken()));

        mockedAppRepository.Verify(m => m.Problems.Update(
            It.IsAny<ProblemEntity>()), Times.Never);
        mockedAppRepository.Verify(m => m.SaveChangesAsync(), Times.Never());
    }
}
