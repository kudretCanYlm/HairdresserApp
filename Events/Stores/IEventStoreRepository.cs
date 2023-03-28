using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Stores
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent @event);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}
