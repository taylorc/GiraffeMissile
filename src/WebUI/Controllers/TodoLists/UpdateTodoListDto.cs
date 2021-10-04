using GiraffeMissile.Application.TodoLists.Commands.UpdateTodoList;
using Microsoft.AspNetCore.Mvc;

namespace GiraffeMissile.WebUI.Controllers.TodoLists
{
    public class UpdateTodoListDto
    {
        [FromRoute(Name="id")]public int Id { get; set; }
        [FromBody]public UpdateTodoListCommand Command { get; set; }
    }
}
