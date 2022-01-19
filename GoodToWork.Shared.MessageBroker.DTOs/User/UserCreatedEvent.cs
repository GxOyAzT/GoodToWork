﻿using GoodToWork.Shared.MessageBroker.DTOs.Shared;

namespace GoodToWork.Shared.MessageBroker.DTOs.User;

public class UserCreatedEvent : BaseEvent
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
