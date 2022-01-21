using MassTransit;
using MassTransit.RabbitMqTransport;

namespace DemandManagement.MessageContracts.Consts
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(RabbitMqConsts.RabbitMqUri), hst =>
                    {
                        hst.Username(RabbitMqConsts.UserName);
                        hst.Password(RabbitMqConsts.Password);
                        hst.RequestedConnectionTimeout(10000);
                    });

                registrationAction?.Invoke(cfg);
            });
        }
    }
}
