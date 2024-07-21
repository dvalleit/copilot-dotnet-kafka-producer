// Step 1: Install the Confluent.Kafka NuGet package
// Run the following command in the terminal:
// dotnet add package Confluent.Kafka

using System;
using Confluent.Kafka;

class Program
{
    public static void Main(string[] args)
    {
        // Step 2: Create a configuration object for the producer
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        // Step 3: Create a producer instance using the configuration
        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            // Step 4: Send a message to the desired topic
            try
            {
                var deliveryResult = producer.ProduceAsync("my-topic", new Message<Null, string> { Value = "Hello, Kafka!" }).Result;

                // Step 5: Handle message delivery reports and errors
                Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}