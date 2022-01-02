using GoodToWork.TasksOrganizer.Application.ApiModels.Project;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.TasksOrganizer.Application.Tests.Tests.ApiModels.Project;

public class ProjectValidationModelTests
{
    [Fact]
    public async Task Invalid_Name()
    {
        var projectValidationModel = new ProjectValidationModel();

        projectValidationModel.InvalidName("Name invalid for some reason.");

        Assert.False(projectValidationModel.IsValid);
    }

    [Fact]
    public async Task Invalid_Description()
    {
        var projectValidationModel = new ProjectValidationModel();

        projectValidationModel.InvalidDescription("Description invalid for some reason.");

        Assert.False(projectValidationModel.IsValid);
    }

    [Fact]
    public async Task Invalid_Description_And_Name()
    {
        var projectValidationModel = new ProjectValidationModel();

        projectValidationModel.InvalidDescription("Description invalid for some reason.");
        projectValidationModel.InvalidName("Name invalid for some reason.");

        Assert.False(projectValidationModel.IsValid);
    }

    [Fact]
    public async Task ValidModel()
    {
        var projectValidationModel = new ProjectValidationModel();

        Assert.True(projectValidationModel.IsValid);
    }
}
