using GoodToWork.NoticicationService.Application.Tests.Mocked.CurrentDateTime;
using GoodToWork.NotificationService.Application.Features.Email.Commands;
using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Application.Repositories.Email;
using GoodToWork.NotificationService.Application.Repositories.User;
using GoodToWork.NotificationService.Domain.Entities;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using GoodToWork.NotificationService.Domain.Exceptions.Input;

namespace GoodToWork.NoticicationService.Application.Tests.Tests.Features.Email.Commands;

public class CreateEmailHandlerTests
{
    [Fact]
    public async Task CannotFindRecipient()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockedUserRepo = new Mock<IUserRepository>();
        var mockedEmailsRepo = new Mock<IEmailRepository>();

        mockedAppRepo.Setup(mar => mar.Users).Returns(mockedUserRepo.Object);
        mockedAppRepo.Setup(mer => mer.Emails).Returns(mockedEmailsRepo.Object);

        var mockedCurrentDateTime = CurrentDateTimeMocked.MockedCurrentDateTime();

        mockedUserRepo.Setup(mur => mur.FindById(It.IsAny<Guid>()))
            .Returns(Task.FromResult((UserEntity)null));

        var createEmailCommand = new CreateEmailCommand(Guid.Empty, "Title", "Contents");

        var testedUnit = new CreateEmailHandler(
            mockedAppRepo.Object, mockedCurrentDateTime.Object);

        await Assert.ThrowsAsync<IncorrectInputException>(() => testedUnit.Handle(createEmailCommand, new CancellationToken()));

        mockedEmailsRepo.Verify(mer => mer.Insert(It.IsAny<EmailEntity>()), Times.Never);
    }

    [Fact]
    public async Task CorrectInsert()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockedUserRepo = new Mock<IUserRepository>();
        var mockedEmailsRepo = new Mock<IEmailRepository>();

        mockedAppRepo.Setup(mar => mar.Users).Returns(mockedUserRepo.Object);
        mockedAppRepo.Setup(mar => mar.Emails).Returns(mockedEmailsRepo.Object);

        var mockedCurrentDateTime = CurrentDateTimeMocked.MockedCurrentDateTime();

        mockedUserRepo.Setup(mur => mur.FindById(It.IsAny<Guid>()))
            .Returns(Task.FromResult(new UserEntity() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001") }));

        var createEmailCommand = new CreateEmailCommand(Guid.Empty, "Title", "Contents");

        var testedUnit = new CreateEmailHandler(
            mockedAppRepo.Object, mockedCurrentDateTime.Object);

        await testedUnit.Handle(createEmailCommand, new CancellationToken());

        mockedEmailsRepo.Verify(mer => mer.Insert(It.Is<EmailEntity>(ee => ee.Title == "Title" && ee.Contents == "Contents" && !ee.WasSent && ee.Recipient.Id == Guid.Parse("00000000-0000-0000-0000-000000000001"))), Times.Once);
    }
}
