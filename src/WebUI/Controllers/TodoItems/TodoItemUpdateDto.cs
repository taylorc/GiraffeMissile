using GiraffeMissile.Application.TodoItems.Commands.UpdateTodoItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GiraffeMissile.WebUI.Controllers.TodoItems
{
    public class TodoItemUpdateDto
    {
        [FromRoute(Name="id")]public int Id { get; set; }
        [FromBody]public UpdateTodoItemCommand Command { get; set; }       
    }
}
