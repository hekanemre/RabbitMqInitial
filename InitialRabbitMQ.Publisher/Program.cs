//At first we need a library (nuget pack.) that communicate with RabbitMQ --> RabbitMQ.Client

#region Connection And Channel Arrangements
//If I want to send data to rabbitmq, i need to provide a connection
var factory = new ConnectionFactory();
//We get that url from rabbitmq cloud(https://api.cloudamqp.com/console/1b5d8175-d770-462b-b89e-b221bfee6fe0/details)
factory.Uri = new Uri("amqps://praqhwrl:FlL17KQ2pUS2kKznDABu8LtzdDDZ3jER@toad.rmq.cloudamqp.com/praqhwrl");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare("initial-queue", true, false, false);
#endregion Connection And Channel Arrangements

#region Publish Message Arrangements and Sending
//we send messages to the queue as byte so that we can sen every kind of data (pdf,img etc.). We need to convert it to byte.
//string message = "first message";
//var messageBody = Encoding.UTF8.GetBytes(message);

//string.Empty make exchange as default exchange so we need to give queue name so default exchange send message to related queue
//channel.BasicPublish(string.Empty, "initial-queue", null, messageBody);


Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    string message = $"sender message: {x}";
    var messageBody = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(string.Empty, "initial-queue", null, messageBody);

});
#endregion Publish Message Arrangements and Sending




Console.WriteLine("Message sent.");
Console.ReadLine();