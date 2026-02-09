// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Confluent.Kafka;
using Kafka.Producer;
using Kafka.Share;

var producerConfig = new ProducerConfig()
{
    BootstrapServers = "localhost:9092",
};

var ids = Enumerable.Range(1, 20).Select(x=>Guid.NewGuid()).ToList();

Console.WriteLine("Hello, World!");
var producerBuilder = new ProducerBuilder<Guid, BaseEvent>(producerConfig);
producerBuilder.SetKeySerializer(new GuidSerializer());
producerBuilder.SetValueSerializer(new KafkaJsonSerializer());
var producer =  producerBuilder.Build();
var kafkaProducer = new KafkaProducer(producer);
var kafkaPublisher = new KafkaPublisher(kafkaProducer);

var i = 1;
while (true)
{
    var random = Random.Shared.Next(ids.Count);
    var id = ids[random];
    var evnt = new EventFactory().CreateRandomEvent(random, id);
    await kafkaPublisher.PublishAsync(evnt, CancellationToken.None);
    await Task.Delay(Random.Shared.Next(0, 5000));
    i++;
}

namespace Kafka.Producer
{
    
    public interface IEventProducer<T>
    {
        Task ProduceAsync(string topic, T message, CancellationToken token);
        Guid GetKey(T message);
    }

    public abstract class GenericKafkaProducer<T> : IEventProducer<T>
    {
        private readonly IProducer<Guid, T> _producer;

        public GenericKafkaProducer(IProducer<Guid, T> producer)
        {
            _producer = producer;
        }
        public async Task ProduceAsync(string topic, T message, CancellationToken token)
        {
            await _producer.ProduceAsync(
                topic,
                new Message<Guid, T> { Key = GetKey(message), Value = message },
                token);
        }
        
        public abstract Guid GetKey(T message);
    }

    public class KafkaProducer : GenericKafkaProducer<BaseEvent>
    {
        public KafkaProducer(IProducer<Guid, BaseEvent> producer) : base(producer)
        {
        }

        public override Guid GetKey(BaseEvent message)
        {
            return message.EntityId;
        }
    }

    public interface IPublisher<T>
    {
        Task PublishAsync(T message, CancellationToken cancellationToken);
    }

    public abstract class AbstractPublisher<T> : IPublisher<T>
    {
        protected readonly IEventProducer<T> Producer;

        public AbstractPublisher(IEventProducer<T> producer)
        {
            Producer = producer;
        }
        public async Task PublishAsync(T @event, CancellationToken token)
        {
            var topic = GetTopic(@event);
            await Producer.ProduceAsync(topic, @event, token);
        }

        protected abstract string GetTopic(T message); 
    }

    public class KafkaPublisher : AbstractPublisher<BaseEvent>
    {
        public KafkaPublisher(IEventProducer<BaseEvent> producer) : base(producer)
        {
        }

        protected override string GetTopic(BaseEvent @event)
        {
            return @event.GetType().Name;
        }

        
    }

    public class EventFactory
    {
        public BaseEvent CreateRandomEvent(int random, Guid entityId)
        {
            var faker = new Faker();
            return (random % 2) switch
            {
                0 => new PersonCreated
                {
                    BirthDay = faker.Person.DateOfBirth,
                    EntityId = entityId,
                    Name = faker.Name.FullName(),
                    Type =  faker.Person.Gender.ToString(),
                },
                1 => new PersonDeleted
                {
                    DeletedOn =  faker.Date.Past(),
                    EntityId = entityId,
                    Reason =  faker.Lorem.Paragraph(),
                }
            };
        }
    }
}


