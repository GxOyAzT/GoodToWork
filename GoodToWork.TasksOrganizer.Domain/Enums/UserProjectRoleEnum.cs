namespace GoodToWork.TasksOrganizer.Domain.Enums;

[Flags]
public enum UserProjectRoleEnum
{
    Performer = 1,
    Creator = 2,
    Moderator = 4
}
