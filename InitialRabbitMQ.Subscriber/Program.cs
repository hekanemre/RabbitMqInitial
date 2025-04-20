//At first we need a library (nuget pack.) that communicate with RabbitMQ --> RabbitMQ.Client

#region Connection And Channel Arrangements
//If I want to send data to rabbitmq i need to provide a connection
var factory = new ConnectionFactory();
//We get that url from rabbitmq cloud
factory.Uri = new Uri("amqps://praqhwrl:FlL17KQ2pUS2kKznDABu8LtzdDDZ3jER@toad.rmq.cloudamqp.com/praqhwrl");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare("initial-queue", true, false, false);
#endregion Connection And Channel Arrangements


#region Subscriber Arrangements
var subscriber = new EventingBasicConsumer(channel);
channel.BasicConsume("initial-queue", true, subscriber);
#endregion Subscriber Arrangements

subscriber.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine("Gelen mesaj: " + message);
};

Console.ReadLine();