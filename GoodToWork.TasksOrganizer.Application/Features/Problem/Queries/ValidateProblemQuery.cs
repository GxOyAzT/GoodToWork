using GoodToWork.TasksOrganizer.Application.ApiModels.Problem;
using MediatR;

namespace GoodToWork.TasksOrganizer.Infrastructure.Features.Problem.Queries;

public sealed record ValidateProblemQuery(string Title, string Description) : IRequest<ProblemValidationModel>;

public sealed class ValidateProblemHandler : IRequestHandler<ValidateProblemQuery, ProblemValidationModel>
{
    ProblemValidationModel ProblemValidationModel = new ProblemValidationModel();

    public Task<ProblemValidationModel> Handle(ValidateProblemQuery request, CancellationToken cancellationToken)
    {
        if (request.Title == null)
        {
            ProblemValidationModel.Title = "Title cannot be empty.";
        }
        else
        {
            if (request.Title.Length < 2 || request.Title.Length > 30)
            {
                ProblemValidationModel.Title = "Title has to be between 2 and 30 characters.";
            }
        }

        if (request.Description == null)
        {
            ProblemValidationModel.Description = "Title cannot be empty.";
        }
        else
        {
            if (request.Description.Length < 2 || request.Description.Length > 300)
            {
                ProblemValidationModel.Description = "Title has to be between 2 and 300 characters.";
            }
        }

        return Task.FromResult(ProblemValidationModel);
    }
}