using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyWebApplication.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly string _topic;

        private readonly IConsumer<Null, string> _kafkaConsumer;

        private readonly IServiceProvider _serviceProvider;

        public KafkaConsumerService(IConfiguration config, IServiceProvider serviceProvider)
        {
            var consumerConfig = new ConsumerConfig();
            config.GetSection("Kafka:ConsumerSettings").Bind(consumerConfig);

            _topic = config.GetValue<string>("Kafka:TopicName");

            _kafkaConsumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => StartConsumerLoop(stoppingToken), stoppingToken);
        }

        private async Task StartConsumerLoop(CancellationToken cancellationToken)
        {
            _kafkaConsumer.Subscribe(_topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _kafkaConsumer.Consume(cancellationToken);
                    var ip = cr.Message.Value;

                    // Handle message...
                    Console.WriteLine($"Message key: {cr.Message.Key}, IP: {cr.Message.Value}");
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    if (e.Error.IsFatal)
                    {
                        // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                        break;
                    }
                }
                catch (Exception e)
                {
                    break;
                }
            }
        }

        public override void Dispose()
        {
            _kafkaConsumer.Close(); // Commit offsets and leave the group cleanly.
            _kafkaConsumer.Dispose();

            base.Dispose();
        }
    }
}
