using Ardalis.Specification;
using GiraffeMissile.Application.TodoLists.Queries.ExportTodos;
using GiraffeMissile.Domain.Entities;
using System.Threading;

namespace GiraffeMissile.Application.sp
{
    public class TodoItemByListIdSpecification : Specification<TodoItem>
    {
        public TodoItemByListIdSpecification(int requestListId)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Query.Where(t => t.ListId == requestListId);
        }
    }
}