using GoodToWork.Shared.Common.Domain.Exceptions.Access;
using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.Shared.Common.Domain.Exceptions.Validation;
using GoodToWork.TasksOrganizer.Application.Features.Comment.Command;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Problem;
using GoodToWork.TasksOrganizer.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.Comment.Command;

public class CreateCommentHandlerTests
{
    [Fact]
    public async Task ProblemDoNotExists()
    {
        var mockedRepos = MockedRepositoriesFactory(null);
        var mockedCurrentDateTime = FakeCurrentDateTime();

        var testedUnit = new CreateCommentHandler(mockedRepos.Item1.Object, mockedCurrentDateTime);

        var createCommentCommand = new CreateCommentCommand("Comment", Guid.Empty, Guid.Empty);

        await Assert.ThrowsAsync<CannnotFindException>(() => testedUnit.Handle(createCommentCommand, new CancellationToken()));

        mockedRepos.Item1.Verify(mar => mar.SaveChangesAsync(), Times.Never());
    }

    [Fact]
    public async Task CommentIsEmpty()
    {
        var mockedRepos = MockedRepositoriesFactory(new ProblemEntity());
        var mockedCurrentDateTime = FakeCurrentDateTime();

        var testedUnit = new CreateCommentHandler(mockedRepos.Item1.Object, mockedCurrentDateTime);

        var createCommentCommand = new CreateCommentCommand("", Guid.Empty, Guid.Empty);

        await Assert.ThrowsAsync<ValidationFailedException>(() => testedUnit.Handle(createCommentCommand, new CancellationToken()));

        mockedRepos.Item1.Verify(mar => mar.SaveChangesAsync(), Times.Never());
    }

    [Fact]
    public async Task SenderDoNotOwnProblem()
    {
        var mockedRepos = MockedRepositoriesFactory(new ProblemEntity() { CreatorId = Guid.Parse("00000000-0000-0000-0000-000000000001"), PerformerId = Guid.Parse("00000000-0000-0000-0000-000000000002")});
        var mockedCurrentDateTime = FakeCurrentDateTime();

        var testedUnit = new CreateCommentHandler(mockedRepos.Item1.Object, mockedCurrentDateTime);

        var createCommentCommand = new CreateCommentCommand("Comment", Guid.Empty, Guid.Parse("00000000-0000-0000-0000-000000000003"));

        await Assert.ThrowsAsync<NoAccessException>(() => testedUnit.Handle(createCommentCommand, new CancellationToken()));

        mockedRepos.Item1.Verify(mar => mar.SaveChangesAsync(), Times.Never());
    }

    [Fact]
    public async Task CorrectCreateRequest()
    {
        var mockedRepos = MockedRepositoriesFactory(new ProblemEntity() { CreatorId = Guid.Parse("00000000-0000-0000-0000-000000000001"), PerformerId = Guid.Parse("00000000-0000-0000-0000-000000000002") });
        var mockedCurrentDateTime = FakeCurrentDateTime();

        var testedUnit = new CreateCommentHandler(mockedRepos.Item1.Object, mockedCurrentDateTime);

        var createCommentCommand = new CreateCommentCommand("Comment", Guid.Empty, Guid.Parse("00000000-0000-0000-0000-000000000002"));

        await testedUnit.Handle(createCommentCommand, new CancellationToken());

        mockedRepos.Item1.Verify(mar => mar.SaveChangesAsync(), Times.Once());
    }

    public ICurrentDateTime FakeCurrentDateTime()
    {
        var currentDateTimeMocked = new Mock<ICurrentDateTime>();

        currentDateTimeMocked.Setup(cdt => cdt.CurrentDateTime)
            .Returns(new DateTime(2022, 1, 12));

        return currentDateTimeMocked.Object;
    }

    private (Mock<IAppRepository>, Mock<IProblemRepository>) MockedRepositoriesFactory(ProblemEntity? problemEntity)
    {
        var mockProblemRepo = new Mock<IProblemRepository>();
        var mockAppRepo = new Mock<IAppRepository>();

        mockProblemRepo.Setup(mpr => mpr.Find(It.IsAny<Func<ProblemEntity, bool>>()))
            .Returns(Task.FromResult(problemEntity));

        mockAppRepo.Setup(mar => mar.Problems)
            .Returns(mockProblemRepo.Object);

        return (mockAppRepo, mockProblemRepo);
    }
}
