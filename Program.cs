using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Testons;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest", Port = 5672 };
//     using(var connection = factory.CreateConnection())
//     using(var channel = connection.CreateModel())
//     {
//         channel.QueueDeclare(queue: "hello",
//                              durable: false,
//                              exclusive: false,
//                              autoDelete: false,
//                              arguments: null);

//         var consumer = new EventingBasicConsumer(channel);
//         consumer.Received += (model, ea) =>
//         {
//             Console.WriteLine("ok");
//             // var body = ea.Body;
//             // var message = Encoding.UTF8.GetString(body);
//             // Console.WriteLine(" [x] Received {0}", message);
//         };
//         channel.BasicConsume(queue: "hello",
//                              autoAck: true,
//                              consumer: consumer);

//         Console.WriteLine(" Press [enter] to exit.");
//         Console.ReadLine();
//     }


var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest", Port = 5672, DispatchConsumersAsync = true  }; 
var connection = factory.CreateConnection(); 
using var channel = connection.CreateModel();
channel.QueueDeclare("orders", true);

// Console.WriteLine("ok");

// var consumer = new AsyncEventingBasicConsumer(channel);

// System.Threading.Thread.Sleep(10000);


// consumer.Received += (model, ea) =>
// {
//     var body = ea.Body.ToArray();
//     var message = Encoding.UTF8.GetString(body);
//     Console.WriteLine(" [x] Received {0}", message);
// };
// Console.WriteLine(consumer);
// Console.WriteLine("ok");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.Received += Testons.Testons.Consumer_Received;
channel.BasicConsume("orders", true, consumer);






// // var consumer = new AsyncEventingBasicConsumer(Channel);
// var consumer = new EventingBasicConsumer(channel);
// Console.WriteLine("ok");
// consumer.Received += (model, eventArgs) =>
// {
//     // var body = eventArgs.Body.ToArray();
//     // var message = Encoding.UTF8.GetString(body);
//     Console.WriteLine(model);
//     Console.WriteLine(eventArgs);
// };

app.Run();

