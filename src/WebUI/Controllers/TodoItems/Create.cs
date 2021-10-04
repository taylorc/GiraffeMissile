using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using GiraffeMissile.Application.TodoItems.Commands.CreateTodoItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoItems
{
    
    [Route("todoitems")]
    public class Create: BaseAsyncEndpoint
        .WithRequest<CreateTodoItemCommand>
        .WithResponse<int>
    {
        private readonly ISender _sender;

        public Create(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost, OpenApiOperation("TodoItems_Create", "Creates a Todo Item", "Creates a Todo Item"), OpenApiTags("TodoItems")]
        public override async Task<ActionResult<int>> HandleAsync([FromBody]CreateTodoItemCommand request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _sender.Send(request, cancellationToken);
        }
    }
}
