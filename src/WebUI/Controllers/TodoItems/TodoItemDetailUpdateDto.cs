using GiraffeMissile.Application.TodoItems.Commands.UpdateTodoItemDetail;
using Microsoft.AspNetCore.Mvc;

namespace GiraffeMissile.WebUI.Controllers.TodoItems
{
    public class TodoItemDetailUpdateDto
    {
        [FromRoute(Name="id")]public int Id { get; set; }
        [FromBody]public UpdateTodoItemDetailCommand Command { get; set; }       
    }
}
