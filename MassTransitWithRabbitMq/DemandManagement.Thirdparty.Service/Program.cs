using DemandManagement.MessageContracts.Consts;
using DemandManagement.Thirdparty.Service;
using MassTransit;

Console.Title = "ThidrParty";

var bus = BusConfigurator.ConfigureBus((cfg) =>
{
    cfg.ReceiveEndpoint(RabbitMqConsts.ThirdPartyServiceQueue, e =>
    {
        e.Consumer<DemandRegisteredEventConsumer>();
    });
});

bus.StartAsync();
Console.WriteLine("Listening for Demand registered events.." + Environment.NewLine + "Press enter to exit");
Console.ReadLine();
bus.StopAsync();