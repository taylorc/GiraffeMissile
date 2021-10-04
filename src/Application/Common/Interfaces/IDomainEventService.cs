using System.Threading.Tasks;
using GiraffeMissile.Domain.Common;

namespace GiraffeMissile.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
