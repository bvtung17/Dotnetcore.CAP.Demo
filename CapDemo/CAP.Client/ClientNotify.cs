using DotNetCore.CAP;

namespace CAP.Client
{
    public class ClientNotify : ICapSubscribe
    {
        [CapSubscribe("helloMethodRabbitMQ", Group = "group1")] // Direct cùng key "helloMethodRabbitMQ" rabbitmq. Group = Name queue
        public void Notify(string message)
        {
            Console.WriteLine($"Show ra : {message}");
        }

        [CapSubscribe("helloMethodRabbitMQ", Group = "group2")]
        public void Notify2(string message)
        {
            Console.WriteLine($"Show ra 2 : {message}");
        }

        [CapSubscribe("helloMethodRabbitMQ", Group = "group3")]
        public void Notify3(string message)
        {
            Console.WriteLine($"Show ra 3 : {message}");
        }
    }
}
