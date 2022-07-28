using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
};

using (var connection = factory.CreateConnection())
{
    using(var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "mainQueue", durable: false, 
            exclusive: false, autoDelete: false, arguments: null);

        string message = "Queue message";

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "", routingKey: "mainQueue",
            basicProperties: null, body: body);

        Console.WriteLine($"Message Sent: {message}");
    }
}
