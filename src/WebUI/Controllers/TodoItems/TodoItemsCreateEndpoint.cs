using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using GiraffeMissile.Application.TodoItems.Commands.CreateTodoItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoItems
{
    [OpenApiController("TodoItems")]
    [Route("todoitems")]
    public class TodoItemsCreateEndpoint: BaseAsyncEndpoint
        .WithRequest<CreateTodoItemCommand>
        .WithResponse<int>
    {
        private readonly ISender _sender;

        public TodoItemsCreateEndpoint(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost, OpenApiOperation("Create", "Creates a Todo Item", "Creates a Todo Item"), OpenApiTags("TodoItems")]
        public override async Task<ActionResult<int>> HandleAsync([FromBody]CreateTodoItemCommand request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _sender.Send(request, cancellationToken);
        }
    }
}
