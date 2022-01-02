using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;

namespace GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime;

internal class GetCurrentDateTime : ICurrentDateTime
{
    public DateTime CurrentDateTime => new DateTime();
}
