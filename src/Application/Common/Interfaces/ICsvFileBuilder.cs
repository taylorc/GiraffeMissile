using System.Collections.Generic;
using GiraffeMissile.Application.TodoLists.Queries.ExportTodos;

namespace GiraffeMissile.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
