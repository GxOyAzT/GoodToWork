using GoodToWork.TasksOrganizer.Infrastructure.Features.Problem.Queries;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.Problem.Queries;
public class ValidateProbemTests
{
    [Fact]
    public async Task CorrectInput()
    {
        var title = "valid titile";
        var decription = "valid description";

        var validationResult = await new ValidateProblemHandler().Handle(new ValidateProblemQuery(title, decription), new CancellationToken());

        Assert.True(validationResult.IsValid);

        Assert.Null(validationResult.Title);
        Assert.Null(validationResult.Description);
    }

    [Fact]
    public async Task TooShort_Title()
    {
        var title = "i";
        var decription = "valid description";

        var validationResult = await new ValidateProblemHandler().Handle(new ValidateProblemQuery(title, decription), new CancellationToken());

        Assert.False(validationResult.IsValid);

        Assert.NotNull(validationResult.Title);
        Assert.Null(validationResult.Description);
    }

    [Fact]
    public async Task TooLong_Title()
    {
        var title = "abcabcabcabcabcabcabcabcabcabcabca";
        var decription = "valid description";

        var validationResult = await new ValidateProblemHandler().Handle(new ValidateProblemQuery(title, decription), new CancellationToken());

        Assert.False(validationResult.IsValid);

        Assert.NotNull(validationResult.Title);
        Assert.Null(validationResult.Description);
    }

    [Fact]
    public async Task TooShort_Description()
    {
        var title = "valid titile";
        var decription = "i";

        var validationResult = await new ValidateProblemHandler().Handle(new ValidateProblemQuery(title, decription), new CancellationToken());

        Assert.False(validationResult.IsValid);

        Assert.Null(validationResult.Title);
        Assert.NotNull(validationResult.Description);
    }
}