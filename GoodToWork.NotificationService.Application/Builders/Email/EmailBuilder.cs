using GoodToWork.NotificationService.Application.Features.CurrentDateTime;
using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Application.Builders.Email;

internal class EmailBuilder :
    IWithTitle,
    IWithContents,
    IWithRecipient,
    IBuild
{
    private readonly ICurrentDateTime currentDateTime;

    private EmailEntity Email { get; set; }

    private EmailBuilder(
        ICurrentDateTime currentDateTime)
    {
        Email = new EmailEntity();
        this.currentDateTime = currentDateTime;
    }

    public static IWithTitle Create(ICurrentDateTime currentDateTime)
    {
        return new EmailBuilder(currentDateTime);
    }

    public IWithContents AddTitle(string title)
    {
        Email.Title = title;
        return this;
    }

    public IWithRecipient AddContents(string contents)
    {
        Email.Contents = contents;
        return this;
    }

    public IBuild AddRecipient(UserEntity recipient)
    {
        Email.Recipient = recipient;
        return this;
    }

    public EmailEntity Build()
    {
        Email.CreationDate = currentDateTime.CurrentDateTime;
        Email.WasSent = false;
        return Email;
    }
}

internal interface IWithTitle
{
    IWithContents AddTitle(string title);
}

internal interface IWithContents
{
    IWithRecipient AddContents(string contents);
}

internal interface IWithRecipient
{
    IBuild AddRecipient(UserEntity recipient);
}

internal interface IBuild
{
    EmailEntity Build();
}