using System.Text;
using Confluent.Kafka;

namespace Kafka.Share;

public class BaseEvent
{
    public Guid EntityId { get; set; }
}

public class PersonCreated : BaseEvent
{
    public string Name { get; set; }
    public DateTime BirthDay { get; set; }
    public string Type { get; set; }
}

public class PersonDeleted : BaseEvent
{
    public string Reason { get; set; }
    public DateTime DeletedOn { get; set; }
}



public class GuidSerializer : IAsyncSerializer<Guid>
{
    public Task<byte[]> SerializeAsync(Guid data, SerializationContext context)
    {
        return Task.FromResult(Encoding.UTF8.GetBytes(data.ToString()));
    }
}
    
public class KafkaJsonSerializer : IAsyncSerializer<BaseEvent>
{
    public Task<byte[]> SerializeAsync(BaseEvent data, SerializationContext context)
    {
        var rr = System.Text.Json.JsonSerializer.Serialize((object)data);
        var tt = Encoding.UTF8.GetBytes(rr);
        return Task.FromResult(tt);
    }
}

public class GuidDeserializer : IDeserializer<Guid>
{
    public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return Guid.Parse(Encoding.UTF8.GetString(data.ToArray()));
    }
}

public class KafkaJsonDeserializer<T> : IDeserializer<T> where T : class
{

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        var ii = Encoding.UTF8.GetString(data.ToArray());
        var oo = System.Text.Json.JsonSerializer.Deserialize<T>(ii);
        return oo;
    }
}


public interface IEventConsumer<T>
{
    T Consume();
}

public interface ISubscriber<T>
{
    Task SubscribeAsync(string topic, Func<T, Task> callback);
}

public class KafkaConsumer<T> : IEventConsumer<T> where T : class
{
    IConsumer<Guid,T> _consumer;
    public KafkaConsumer(ConsumerConfig config)
    {
        var builder = new ConsumerBuilder<Guid, T>(config);
        builder.SetKeyDeserializer(new GuidDeserializer());
        builder.SetValueDeserializer(new KafkaJsonDeserializer<T>());
        _consumer = builder.Build();
        _consumer.Subscribe( typeof(T).Name);
    }
    public T Consume()
    {
        var msg = _consumer.Consume();
        return msg.Message.Value;
    }
}

public class GenericSubscriber<T> : ISubscriber<T>
{
    private readonly IEventConsumer<T> _consumer;

    public GenericSubscriber(IEventConsumer<T> consumer)
    {
        _consumer = consumer;
    }
    public async Task SubscribeAsync(string topic, Func<T, Task> callback)
    {
        var msg = _consumer.Consume();
        await callback(msg);
    }
}

public class KafkaGenericSubscriber<T> : GenericSubscriber<T> where T : class
{
    public KafkaGenericSubscriber(KafkaConsumer<T> consumer) : base(consumer)
    {
    }
}
