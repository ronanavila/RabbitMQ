using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
};

using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "mainQueue", durable: false,
            exclusive: false, autoDelete: false, arguments: null);

        var consumerEventing = new EventingBasicConsumer(channel);

        consumerEventing.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Message Received: {message}");
        };

        channel.BasicConsume(queue: "mainQueue", autoAck: true, consumer: consumerEventing);

        Console.ReadLine();
        
    }
}
