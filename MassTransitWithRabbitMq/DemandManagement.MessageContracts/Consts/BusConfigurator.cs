using MassTransit;
using MassTransit.RabbitMqTransport;

namespace DemandManagement.MessageContracts.Consts
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(RabbitMqConsts.RabbitMqUri), hst =>
                    {
                        hst.Username(RabbitMqConsts.UserName);
                        hst.Password(RabbitMqConsts.Password);
                        hst.RequestedConnectionTimeout(10000);
                    });


                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}
