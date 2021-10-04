using System.Collections.Generic;
using GiraffeMissile.Domain.Common;
using GiraffeMissile.Domain.ValueObjects;

namespace GiraffeMissile.Domain.Entities
{
    public class TodoList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Colour Colour { get; set; } = Colour.White;

        public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}
