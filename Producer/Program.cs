using RabbitMQ.Client;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "qwebapp",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        int count = 0;
        while (true)
        {
            string message = $"{count++} Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "qwebapp",
                                 basicProperties: null,
                                 body: body);
            Thread.Sleep(200);
        }
    }
}