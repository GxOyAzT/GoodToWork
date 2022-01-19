using GoodToWork.Shared.MessageBroker.Application.Interfaces;
using GoodToWork.Shared.MessageBroker.DTOs.Shared;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;

namespace GoodToWork.Shared.MessageBroker.Infrastructure.Builder;

public class MessageBrokerBuilder
{
    private IConnection _connection;
    private IModel _channel;
    Assembly _assembly;
    private IServiceCollection _services;

    public MessageBrokerBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public MessageBrokerBuilder RegisterApplicationLayerAssembly(Assembly assembly)
    {
        _assembly = assembly;

        return this;
    }

    public MessageBrokerBuilder RegisterConnectionUri(string uri)
    {
        var factory = new ConnectionFactory() { Uri = new Uri(uri) };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        return this;
    }

    public MessageBrokerBuilder RegisterListener<TEvent>()
        where TEvent : BaseEvent
    {
        var handlerType = _assembly
            .GetTypes()
            .FirstOrDefault(type => type.IsAssignableTo(typeof(IEventHandler<TEvent>)));

        _services.AddTransient(typeof(IEventHandler<TEvent>), handlerType);

        _channel.ExchangeDeclare(typeof(TEvent).Name, ExchangeType.Fanout);

        var queueName = _channel.QueueDeclare($"{_assembly.GetName().Name}-{typeof(TEvent).Name}", durable: true, autoDelete: false, exclusive: false).QueueName;

        _channel.QueueBind(queueName, typeof(TEvent).Name, "");

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var @event = JsonConvert.DeserializeObject<TEvent>(message);

            var handler = _services.BuildServiceProvider().GetRequiredService<IEventHandler<TEvent>>().HandleAsync(@event);
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        return this;
    }

    public IModel Build() => _channel;
}
