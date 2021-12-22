﻿namespace GoodToWork.TasksOrganizer.Domain.Entities;

public class CommentEntity
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }
    public Guid CreatorId { get; set; }

    public string Comment { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Created { get; set; }
    

    public virtual UserEntity? Creator { get; set; }
    public virtual TaskEntity? Task { get; set; }
}
