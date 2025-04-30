//At first we need a library (nuget pack.) that communicate with RabbitMQ --> RabbitMQ.Client

#region Connection And Channel Arrangements
//If I want to send data to rabbitmq i need to provide a connection
var factory = new ConnectionFactory();
//We get that url from rabbitmq cloud
factory.Uri = new Uri("amqps://praqhwrl:FlL17KQ2pUS2kKznDABu8LtzdDDZ3jER@toad.rmq.cloudamqp.com/praqhwrl");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();
//BasicQos handles the quantity of message that subscriber get. First attribute gives us size of message and we doesn't need this
//specification so we are giving it 0. Second attribute gives us how many message do i want to give subscriber.
//if third one is false everysubscriber get second attribute amount of message from queue. But if it is true 
//second attribute amount of message seperating for subscribers equally if it is profit
channel.BasicQos(0,1,false);
channel.QueueDeclare("initial-queue", true, false, false);
#endregion Connection And Channel Arrangements


#region Subscriber Arrangements
var subscriber = new EventingBasicConsumer(channel);
//If i give second property as "true" if i read messsage correctly or uncorrectly both way this message is removed from queue.
//If i give it "false" it means i say to the rabbitmq you don't remove that message until i give you an information that you can remove
channel.BasicConsume("initial-queue", false, subscriber);
#endregion Subscriber Arrangements

subscriber.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Thread.Sleep(1500);

    channel.BasicAck(e.DeliveryTag, false);

    Console.WriteLine("Gelen mesaj: " + message);
};

Console.ReadLine();