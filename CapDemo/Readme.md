### Getting Started
**Using CAP with RabbitMQ flow.**
*Using with Net 7.0 and rabbitmq*

## Flow
![Markdown Flow.](https://cdn-0.plantuml.com/plantuml/png/PP113e8m44NtFSLS019PkZ0OroGA9nZA3pOAn7OW7jysX6fbs__-xykKJjZAF3nLPRIT6jZ5Q1oo4rtEroCr63kgyPhXHFaNocB22yYk2Grmt8FCA1DrBDooGy03NNFR6gbkwrYiU19lVUUnXr8RG-jwGNluLw_8LQ0scIBQQ9W5DkZiVM8bdKf_obKI1loF7lY1BrtwhZ_U1m00)


## Nuget

**Must have:**
```
PM> Install-Package DotNetCore.CAP
```

**Using with DB:**
```
PM> Install-Package DotNetCore.CAP.MySql
```

**Using message queue for microservices.**

```
PM> Install-Package DotNetCore.CAP.RabbitMQ
```

### Configuration
**Step 0: Setup RabbitMQ with Docker**
    1. Erlang : (https://erlang.org/download/otp_versions_tree.html)
    2. Docker : `docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management`

**Step 1: Setup configuration in your Program.cs (In 2 Microservices)**
```cs
    builder.Services.AddCap(options =>
    {
        options.UseMySql("server=localhost;Port=3306;user=root;password=password123;database=Demo;ConnectionTimeout=120;");
        options.UseRabbitMQ("localhost");
    });
```
**Step 2: Setup MicroService1 with Role Procedure**
1. Implement `ICapPublisher` with DI
    ```cs
        private readonly ICapPublisher _capPublisher;
        public ProcedureController(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }
    ```
2. Create Method Publisher method.
   ```cs
        [HttpPost("helloMethod")]
        public async Task<IActionResult> CallHelloMethod(string message)
        {
            await _capPublisher.PublishAsync("helloMethodRabbitMQ", $"{message} + RabbitMQ");
            return Ok();
        }
   ```
   ** `helloMethodRabbitMQ` this is a name of CAP Method is called with CAP.
   ** `$"{message} + RabbitMQ"` this is a object parameter for consumer method.

**Step 3: Setup MicroService2 with Role Consumer**
1.  Implement interface `ICapSubscribe`.
    ```cs
    public class ClientNotify : ICapSubscribe
    {
        // Methods.
    }
    ```
2. Create Method Subscribe
    ```cs
    [CapSubscribe("helloMethodRabbitMQ")]
    public void Notify(string message)
    {
      Console.WriteLine($"Show ra : {message}");
    }
    ```
    * NOTE: `helloMethodRabbitMQ`