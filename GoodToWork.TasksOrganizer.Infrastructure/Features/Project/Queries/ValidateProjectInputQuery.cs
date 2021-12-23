using GoodToWork.TasksOrganizer.Application.ApiModels.Project;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Validation;
using MediatR;

namespace GoodToWork.TasksOrganizer.Infrastructure.Features.Project.Queries;

internal sealed record ValidateProjectInputQuery(string Name, string Description) : IRequest<Unit>;

internal sealed class ValidateProjectInputHandler : IRequestHandler<ValidateProjectInputQuery, Unit>
{
    public Task<Unit> Handle(ValidateProjectInputQuery request, CancellationToken cancellationToken)
    {
        var validationModel = new ProjectValidationModel();
        if (String.IsNullOrEmpty(request.Name))
        {
            validationModel.InvalidName("Name has to be at least two characters.");
        }
        else
        {
            if (request.Name.Length > 30)
            {
                validationModel.InvalidName("Name cannot be longer then 30 characters.");
            }
        }

        if (!String.IsNullOrEmpty(request.Description))
        {
            if (request.Description.Length > 300)
            {
                validationModel.InvalidName("Description cannot be longer then 300 characters.");
            }
        }

        if (!validationModel.IsValid)
            throw new ValidationFailedError($"Passed object is invalid.", System.Net.HttpStatusCode.BadRequest, validationModel);

        return Task.FromResult(Unit.Value);
    }
}