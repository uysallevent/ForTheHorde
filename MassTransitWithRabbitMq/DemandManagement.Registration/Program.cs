
using DemandManagement.MessageContracts.Consts;
using DemandManagement.Registration;
using MassTransit;

Console.Title = "Register";

var bus = BusConfigurator.ConfigureBus((cfg) =>
{
    cfg.ReceiveEndpoint(RabbitMqConsts.RegisterDemandServiceQueue, e =>
    {
        e.Consumer<RegisterDemandCommandConsumer>();
    });
});

bus.StartAsync();

Console.WriteLine("Listening for Register Demand commands.. " + Environment.NewLine + "Press enter to exit");
Console.ReadLine();

bus.StopAsync();