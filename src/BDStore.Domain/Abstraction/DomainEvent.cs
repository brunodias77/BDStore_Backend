using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDStore.Domain.Abstraction
{
    public class DomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; }

        public DomainEvent()
        {
            this.OccurredOn = DateTime.Now;
        }
    }
}