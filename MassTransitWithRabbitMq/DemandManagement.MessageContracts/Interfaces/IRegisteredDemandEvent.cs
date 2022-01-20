using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandManagement.MessageContracts.Interfaces
{
    public interface IRegisteredDemandEvent
    {
        public Guid DemandId { get; set; }
    }
}
