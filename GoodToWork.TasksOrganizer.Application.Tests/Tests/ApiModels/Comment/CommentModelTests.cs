using GoodToWork.TasksOrganizer.Application.ApiModels.Comment;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Domain.Entities;
using Moq;
using System;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.ApiModels.Comment;

public class CommentModelTests
{
    [Fact]
    public void CreatedToday()
    {
        var mockedCurrentTime = new Mock<ICurrentDateTime>();

        mockedCurrentTime.Setup(cdt => cdt.CurrentDateTime).Returns(new DateTime(2022, 1, 1, 10, 35, 40));

        var commentEntity = new CommentEntity()
        {
            Id = Guid.NewGuid(),
            Comment = "_comment_",
            Created = new DateTime(2022,1,1, 9,41,30)
        };

        var commentModel = new CommentBaseModel(commentEntity, mockedCurrentTime.Object);

        Assert.Equal(commentEntity.Id, commentModel.Id);
        Assert.Equal(commentEntity.Comment, commentModel.Comment);
        Assert.Equal("09:41", commentModel.Created);
    }

    [Fact]
    public void CreatedYesterdayNotLongerThen24Hours()
    {
        var mockedCurrentTime = new Mock<ICurrentDateTime>();

        mockedCurrentTime.Setup(cdt => cdt.CurrentDateTime).Returns(new DateTime(2022, 1, 2, 8, 35, 40));

        var commentEntity = new CommentEntity()
        {
            Id = Guid.NewGuid(),
            Comment = "_comment_",
            Created = new DateTime(2022, 1, 1, 9, 41, 30)
        };

        var commentModel = new CommentBaseModel(commentEntity, mockedCurrentTime.Object);

        Assert.Equal(commentEntity.Id, commentModel.Id);
        Assert.Equal(commentEntity.Comment, commentModel.Comment);
        Assert.Equal("09:41", commentModel.Created);
    }

    [Fact]
    public void CreatedYesterdayLongerThen24Hours()
    {
        var mockedCurrentTime = new Mock<ICurrentDateTime>();

        mockedCurrentTime.Setup(cdt => cdt.CurrentDateTime).Returns(new DateTime(2022, 1, 2, 10, 35, 40));

        var commentEntity = new CommentEntity()
        {
            Id = Guid.NewGuid(),
            Comment = "_comment_",
            Created = new DateTime(2022, 1, 1, 9, 41, 30)
        };

        var commentModel = new CommentBaseModel(commentEntity, mockedCurrentTime.Object);

        Assert.Equal(commentEntity.Id, commentModel.Id);
        Assert.Equal(commentEntity.Comment, commentModel.Comment);
        Assert.Equal("09:41 01-01-2022", commentModel.Created);
    }
}
