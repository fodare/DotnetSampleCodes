using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


// Connection setup
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Decleare queue and receive message. Consummer might start before publisher
// So queue is created regardless of starting node.
channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine($"Consummer is waiting on messages!");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Consumer received message: {message}");

    int dots = message.Split(".").Length - 1;
    Thread.Sleep(dots * 1000);
    Console.WriteLine($"Worker is done");
    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
};
channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();