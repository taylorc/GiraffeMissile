using Ardalis.Specification;
using GiraffeMissile.Domain.Entities;

namespace GiraffeMissile.Application.Specifications
{
    public class TodoListOrderedByTitleAscSpecification : Specification<TodoList>
    {
        public TodoListOrderedByTitleAscSpecification()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Query.AsNoTracking()
                .OrderBy(t => t.Title);
        }
    }
}