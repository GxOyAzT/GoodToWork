using GoodToWork.Shared.Common.Domain.Exceptions.Validation;
using GoodToWork.TasksOrganizer.Application.Features.Project.Queries;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.Features.Project.Queries;

public class ValidateProductInputHandlerTests
{
    [Fact]
    public async Task Name_Null()
    {
        var input = new ValidateProjectInputQuery(null, "");

        var testedUnit = new ValidateProjectInputHandler();

        await Assert.ThrowsAsync<ValidationFailedException>(() => testedUnit.Handle(input, new CancellationToken()));
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

        await Assert.ThrowsAsync<ValidationFailedException>(() => testedUnit.Handle(input, new CancellationToken()));
    }

    [Fact]
    public async Task Description_TooLong()
    {
        var input = new ValidateProjectInputQuery("abc", "abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabca");

        var testedUnit = new ValidateProjectInputHandler();

        await Assert.ThrowsAsync<ValidationFailedException>(() => testedUnit.Handle(input, new CancellationToken()));
    }

    [Fact]
    public async Task Ok()
    {
        var input = new ValidateProjectInputQuery("abc", "");

        var testedUnit = new ValidateProjectInputHandler();

        await testedUnit.Handle(input, new CancellationToken());
    }
}
