using DemandManagement.MessageContracts.Consts;
using DemandManagement.Notification;
using MassTransit;

Console.Title = "Notification";

var bus = BusConfigurator.ConfigureBus((cfg) =>
{

    cfg.ReceiveEndpoint(RabbitMqConsts.NotificationServiceQueue, e =>
    {
        e.Consumer<DemandRegisteredEventConsumer>();
    });
});

bus.StartAsync();
Console.WriteLine("Listening for Demand registered events.."+Environment.NewLine+"Press enter to exit");
Console.ReadLine();
bus.StopAsync();