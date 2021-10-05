using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using GiraffeMissile.Application.Common.Models;
using GiraffeMissile.Domain.Entities;

namespace GiraffeMissile.Application.Specifications
{
    public class TodoItemsByListIdOrderedByTitleAscSpecification: Specification<TodoItem>
    {
        public TodoItemsByListIdOrderedByTitleAscSpecification(int listId)
        {

            // ReSharper disable once VirtualMemberCallInConstructor
            Query.Where(x => x.ListId == listId)
                .OrderBy(x => x.Title);
        }
    }
}
