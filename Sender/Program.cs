using System;
using RabbitMQ.Client;
using System.Text;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var factory = new ConnectionFactory() {HostName="localhost"};
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {   
                    channel.QueueDeclare(queue: "FirstQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                    string message = "My First Message in Queue";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                     routingKey: "RabbitMQ",
                                     basicProperties: null,
                                     body: body);
                    Console.WriteLine("Message is Sent");

                }
            }
            Console.ReadLine();

        }
    }
}
