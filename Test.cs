using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Testons 
{
    class Testons {
    public static async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            // var message = Encoding.UTF8.GetString(@event.Body);

            // Console.WriteLine($"Begin processing {message}");

            await Task.Delay(250);

            Console.WriteLine($"End processing");
        }
    }

}