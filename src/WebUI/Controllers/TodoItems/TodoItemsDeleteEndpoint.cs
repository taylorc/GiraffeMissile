using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using GiraffeMissile.Application.Common.Models;
using GiraffeMissile.Application.TodoItems.Commands.DeleteTodoItem;
using GiraffeMissile.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoItems
{
    
    [Route("todoitems")]
    public class TodoItemsDeleteEndpoint: BaseAsyncEndpoint
        .WithRequest<int>
        .WithoutResponse
    {
        private readonly ISender _sender;

        public TodoItemsDeleteEndpoint(ISender sender)
        {
            _sender = sender;
        }

        [HttpDelete("{id}"),OpenApiOperation("TodoItems_Delete", "Deletes a Todo Item", "Deletes a Todo Item"), OpenApiTags("TodoItems")]
        public override async Task<ActionResult> HandleAsync([FromRoute] int id,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await _sender.Send(new DeleteTodoItemCommand { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
