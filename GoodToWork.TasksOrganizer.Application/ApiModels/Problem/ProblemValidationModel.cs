namespace GoodToWork.TasksOrganizer.Application.ApiModels.Problem;

public class ProblemValidationModel
{
    public string? Title { get; set; } = null;
    public string? Description { get; set; } = null;

    public bool IsValid { get => Title is null && Description is null; }
}