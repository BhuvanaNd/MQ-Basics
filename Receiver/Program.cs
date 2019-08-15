using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Receiver
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
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, msg)=>
                    {
                        var body = msg.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Messge Received: {0} \n",message);
                    };
                    channel.BasicConsume(queue: "FirstQueue",
                                         autoAck: true,
                                         consumer: consumer);
                }
            }
            Console.ReadLine();
        }
    }
}
