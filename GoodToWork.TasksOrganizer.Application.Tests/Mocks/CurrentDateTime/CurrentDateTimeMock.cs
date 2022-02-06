using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using Moq;
using System;

namespace GoodToWork.TasksOrganizer.Application.Tests.Mocks.CurrentDateTime;

internal class CurrentDateTimeMock
{
    public static Mock<ICurrentDateTime> MockOne()
    {
        var mockICurrentDateTime = new Mock<ICurrentDateTime>();
        mockICurrentDateTime.Setup(m => m.CurrentDateTime).Returns(new DateTime(2021, 5, 12, 11, 15, 30));
        return mockICurrentDateTime;
    }
}
