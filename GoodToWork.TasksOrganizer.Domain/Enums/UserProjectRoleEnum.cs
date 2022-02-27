namespace GoodToWork.TasksOrganizer.Domain.Enums;

[Flags]
public enum UserProjectRoleEnum
{
    None = 0,
    Performer = 1,
    Creator = 2,
    Moderator = 4
}
