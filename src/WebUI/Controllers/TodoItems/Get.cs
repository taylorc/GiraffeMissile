using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using GiraffeMissile.Application.Common.Models;
using GiraffeMissile.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoItems
{
    
    [Route("todoitems")]
    public class Get: BaseAsyncEndpoint
        .WithRequest<GetTodoItemsWithPaginationQuery>
        .WithResponse<PaginatedList<TodoItemBriefDto>>
    {
        private readonly ISender _sender;

        public Get(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetTodoItemsWithPagination"), OpenApiOperation("TodoItems_GetTodoItemsWithPagination", "Gets a Todo Item", "Gets a Todo Item"), OpenApiTags("TodoItems")]
        public override async Task<ActionResult<PaginatedList<TodoItemBriefDto>>> HandleAsync([FromBody]GetTodoItemsWithPaginationQuery request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _sender.Send(request, cancellationToken);
        }
    }
}
