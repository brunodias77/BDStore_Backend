using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BDStore.Domain.Abstraction
{
    public interface IDomainEvent : INotification
    {

    }
}