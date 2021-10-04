using GiraffeMissile.Domain.Common;
using GiraffeMissile.Domain.Entities;

namespace GiraffeMissile.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
