using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoItems
{
    [OpenApiController("TodoItems")]
    [Route("todoitems")]
    public class TodoItemsUpdateItemDetailsEndpoint : BaseAsyncEndpoint
        .WithRequest<TodoItemDetailUpdateDto>
        .WithoutResponse
    {
        private readonly ISender _sender;

        public TodoItemsUpdateItemDetailsEndpoint(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("UpdateItemDetails/{id}"), OpenApiOperation("UpdateItemDetail", "Updates a Todo Item Detail", "Updates a Todo Item Detail"), OpenApiTags("TodoItems")]
        public override async Task<ActionResult> HandleAsync([FromRoute] TodoItemDetailUpdateDto request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var id = request.Id;
            var command = request.Command;

            if (id != command.Id)
            {
                return BadRequest();
            }

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
