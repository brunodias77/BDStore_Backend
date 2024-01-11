using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDStore.Domain.Abstraction
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}