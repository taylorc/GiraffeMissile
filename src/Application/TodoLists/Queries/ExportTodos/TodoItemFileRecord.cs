﻿using GiraffeMissile.Application.Common.Mappings;
using GiraffeMissile.Domain.Entities;

namespace GiraffeMissile.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
