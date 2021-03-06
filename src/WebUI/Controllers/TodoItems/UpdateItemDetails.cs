using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoItems
{
    
    [Route("todoitems")]
    public class UpdateItemDetails : BaseAsyncEndpoint
        .WithRequest<UpdateTodoItemDetailsDto>
        .WithoutResponse
    {
        private readonly ISender _sender;

        public UpdateItemDetails(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("UpdateItemDetails/{id}"), OpenApiOperation("TodoItems_UpdateItemDetails", "Updates a Todo Item Detail", "Updates a Todo Item Detail"), OpenApiTags("TodoItems")]
        public override async Task<ActionResult> HandleAsync([FromRoute] UpdateTodoItemDetailsDto request,
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
