namespace GoodToWork.NotificationService.Application.Features.CurrentDateTime;

public interface ICurrentDateTime
{
    DateTime CurrentDateTime { get; }
    DateTime CurrentDate { get; }
}

public class CurrentDateTimeFeature : ICurrentDateTime
{
    public DateTime CurrentDate => DateTime.Now.Date;
    public DateTime CurrentDateTime => DateTime.Now;
}
