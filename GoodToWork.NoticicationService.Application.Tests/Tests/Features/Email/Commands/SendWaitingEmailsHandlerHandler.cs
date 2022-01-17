using GoodToWork.NotificationService.Application.Repositories.Email;
using GoodToWork.NotificationService.Application.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;
using System;
using GoodToWork.NotificationService.Domain.Entities;
using System.Collections.Generic;
using GoodToWork.NotificationService.Application.Email;
using GoodToWork.NotificationService.Application.Features.Email.Commands;
using System.Threading;

namespace GoodToWork.NoticicationService.Application.Tests.Tests.Features.Email.Commands;

public class SendWaitingEmailsHandlerHandler
{
    [Fact]
    public async Task NoEmailsToSend()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockedEmailsRepo = new Mock<IEmailRepository>();
        var mockedEmailSender = new Mock<IEmailSender>();

        mockedAppRepo.Setup(mar => mar.Emails).Returns(mockedEmailsRepo.Object);

        mockedEmailsRepo.Setup(mer => mer.Get(It.IsAny<Func<EmailEntity, bool>>()))
            .Returns(Task.FromResult(new List<EmailEntity>()));

        var testedUnit = new SendWaitingEmailsHandler(mockedAppRepo.Object, mockedEmailSender.Object);

        await testedUnit.Handle(new SendWaitingEmailsCommand(), new CancellationToken());

        mockedEmailSender.Verify(mes => mes.SendEmail(It.IsAny<EmailEntity>()),Times.Never);
        mockedEmailsRepo.Verify(mer => mer.Update(It.IsAny<EmailEntity>()),Times.Never);
    }

    [Fact]
    public async Task OneEmailToSend()
    {
        var mockedAppRepo = new Mock<IAppRepository>();
        var mockedEmailsRepo = new Mock<IEmailRepository>();
        var mockedEmailSender = new Mock<IEmailSender>();

        mockedAppRepo.Setup(mar => mar.Emails).Returns(mockedEmailsRepo.Object);

        mockedEmailsRepo.Setup(mer => mer.Get(It.IsAny<Func<EmailEntity, bool>>()))
            .Returns(Task.FromResult(new List<EmailEntity>()
            {
                new EmailEntity()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Title = "Title"
                }
            }));

        var testedUnit = new SendWaitingEmailsHandler(mockedAppRepo.Object, mockedEmailSender.Object);

        await testedUnit.Handle(new SendWaitingEmailsCommand(), new CancellationToken());

        mockedEmailSender.Verify(mes => mes.SendEmail(It.Is<EmailEntity>(ee => ee.Id == Guid.Parse("00000000-0000-0000-0000-000000000001") && ee.Title == "Title")), Times.Once);
        mockedEmailsRepo.Verify(mer => mer.Update(It.Is<EmailEntity>(ee => ee.Id == Guid.Parse("00000000-0000-0000-0000-000000000001") && ee.Title == "Title" && ee.WasSent)), Times.Once);
    }
}
