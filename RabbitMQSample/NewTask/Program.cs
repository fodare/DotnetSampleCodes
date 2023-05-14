using System.Text;
using RabbitMQ.Client;


public class NewTask
{
    public static void Main(string[] args)
    {
        // Connection setup
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();


        // Decleare queue and send message
        channel.QueueDeclare(queue: "hello",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
        );

        //const string message = "Sender says hello!";
        for (int i = 11; i <= 21; i++)
        {
            var messageBody = Encoding.UTF8.GetBytes($"Producer message no {i}");
            channel.BasicPublish(exchange: string.Empty, routingKey: "hello", basicProperties: null, body: messageBody);
            Console.WriteLine($"Sender sent message. {i}");
        }
        Console.WriteLine($"\nPlease press enter to exit");
        Console.ReadLine();

    }
}



