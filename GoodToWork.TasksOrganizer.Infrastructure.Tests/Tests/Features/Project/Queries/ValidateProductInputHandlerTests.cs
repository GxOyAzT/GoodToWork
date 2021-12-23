using GoodToWork.TasksOrganizer.Domain.Exceptions.Validation;
using GoodToWork.TasksOrganizer.Infrastructure.Features.Project.Queries;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Infrastructure.Tests.Tests.Features.Project.Queries;

public class ValidateProductInputHandlerTests
{
    [Fact]
    public async Task Name_Null()
    {
        var input = new ValidateProjectInputQuery(null, "");

        var testedUnit = new ValidateProjectInputHandler();

        await Assert.ThrowsAsync<ValidationFailedError>(() => testedUnit.Handle(input, new CancellationToken()));
    }

    [Fact]
    public async Task Description_Null()
    {
        var input = new ValidateProjectInputQuery("abc", null);

        var testedUnit = new ValidateProjectInputHandler();

        await testedUnit.Handle(input, new CancellationToken());
    }

    [Fact]
    public async Task Name_TooLong()
    {
        var input = new ValidateProjectInputQuery("abcabcabcabcabcabcabcabcabcabca", null);

        var testedUnit = new ValidateProjectInputHandler();

        await Assert.ThrowsAsync<ValidationFailedError>(() => testedUnit.Handle(input, new CancellationToken()));
    }

    [Fact]
    public async Task Description_TooLong()
    {
        var input = new ValidateProjectInputQuery("abc", "abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabca");

        var testedUnit = new ValidateProjectInputHandler();

        await Assert.ThrowsAsync<ValidationFailedError>(() => testedUnit.Handle(input, new CancellationToken()));
    }

    [Fact]
    public async Task Ok()
    {
        var input = new ValidateProjectInputQuery("abc", "");

        var testedUnit = new ValidateProjectInputHandler();

        await testedUnit.Handle(input, new CancellationToken());
    }
}
