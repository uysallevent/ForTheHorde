using DemandManagement.MessageContracts.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandManagement.Thirdparty.Service
{
    internal class DemandRegisteredEventConsumer : IConsumer<IRegisteredDemandEvent>
    {
        public async Task Consume(ConsumeContext<IRegisteredDemandEvent> context)
        {
            await Console.Out.WriteLineAsync($"Thirdparty integration done: Demand id {context.Message.DemandId}");
        }
    }
}
