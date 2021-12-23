namespace GoodToWork.TasksOrganizer.Application.ApiModels.Project;

public class ProjectValidationModel
{
    public string? Name { get; private set; } = null;
    public string? Description { get; private set; } = null;

    public bool IsValid { get; private set; } = true;

    public void InvalidName(string why)
    {
        IsValid = false;
        Name = why;
    }

    public void InvalidDescription(string why)
    {
        IsValid = false;
        Name = why;
    }
}
