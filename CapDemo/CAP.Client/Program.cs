using CAP.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddCap(options =>
{
    options.UseMySql("server=localhost;Port=3306;user=root;password=password123;database=Demo;ConnectionTimeout=120;");
    options.UseRabbitMQ("localhost");
});

builder.Services.AddTransient<ClientNotify>();

IHost host = builder.Build();
host.Run();