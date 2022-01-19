using GoodToWork.NotificationService.Application.Features.CurrentDateTime;
using Moq;
using System;

namespace GoodToWork.NoticicationService.Application.Tests.Mocked.CurrentDateTime;

internal class CurrentDateTimeMocked
{
    public static Mock<ICurrentDateTime> MockedCurrentDateTime()
    {
        var mockedCurrentDateTime = new Mock<ICurrentDateTime>();

        mockedCurrentDateTime.Setup(mcdt => mcdt.CurrentDateTime).Returns(new DateTime(2022, 5, 16, 14, 25, 38));

        return mockedCurrentDateTime;
    }
}
