using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoItems
{
    
    [Route("todoitems")]
    public class Update: BaseAsyncEndpoint
        .WithRequest<UpdateTodoItemDto>
        .WithoutResponse
    {
        private readonly ISender _sender;

        public Update(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("{id}"), OpenApiOperation("TodoItems_Update", "Updates a Todo Item", "Updates a Todo Item"), OpenApiTags("TodoItems")]
        public override async Task<ActionResult> HandleAsync([FromRoute]UpdateTodoItemDto request,
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
