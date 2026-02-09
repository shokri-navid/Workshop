// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using Kafka.Share;

Console.WriteLine("Hello, World!");
var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId =  "test",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var createConsumer = new KafkaConsumer<PersonCreated>(consumerConfig);
var deleteConsumer = new KafkaConsumer<PersonDeleted>(consumerConfig);

var createSubscriber = new GenericSubscriber<PersonCreated>(createConsumer);
var deleteSubscriber = new GenericSubscriber<PersonDeleted>(deleteConsumer);

while (true)
{
    await createSubscriber.SubscribeAsync("", created =>
    {
        Console.WriteLine($"Message created {created.Name} : {created.Type}  was received.");
        return Task.CompletedTask;
    });
    
     await deleteSubscriber.SubscribeAsync("", deleted =>
    {
        Console.WriteLine($"Message deleted with reason {deleted.Reason}  was received.");
        return Task.CompletedTask;
    });
}


/*var builder = new ConsumerBuilder<Guid, PersonCreated>(consumerConfig);

builder
    .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"));
builder.SetKeyDeserializer(new GuidDeserializer());
builder.SetValueDeserializer(new KafkaJsonDeserializer<PersonCreated>());
var createConsumer1 = builder.Build();
createConsumer.Subscribe(nameof(PersonCreated));

while (true)
{
    var data = createConsumer.Consume();
    if (data != null)
    {
        Console.WriteLine($"Message {data.Message.Value.Name} : {data.Message.Value.Type}  was not received.");
        await Task.Delay(10);
    }
}*/