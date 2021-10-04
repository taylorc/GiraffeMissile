using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using GiraffeMissile.Application.TodoLists.Queries.GetTodos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoLists
{
    [Route("todolists")]
    public class Get: BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<TodosVm>
    {
        private readonly ISender _sender;

        public Get(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet, OpenApiOperation("TodoLists_Get", "Gets a Todo", "Gets a Todo"), OpenApiTags("TodoLists")]
        public override async Task<ActionResult<TodosVm>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await _sender.Send(new GetTodosQuery(), cancellationToken);
        }
    }
}
