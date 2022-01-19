using GoodToWork.Shared.MessageBroker.Application.Interfaces;
using GoodToWork.Shared.MessageBroker.DTOs.Shared;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace GoodToWork.Shared.MessageBroker.Infrastructure.Features;

internal class EventSender : IEventSender
{
    private readonly IModel _model;

    public EventSender(
        IModel model)
    {
        _model = model;
    }

    public Task Send<TEvent>(TEvent @event) where TEvent : BaseEvent
    {
        _model.BasicPublish(
            typeof(TEvent).Name,
            "",
            null,
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)));

        return Task.CompletedTask;
    }
}
