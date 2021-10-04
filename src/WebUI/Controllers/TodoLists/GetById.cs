using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using GiraffeMissile.Application.TodoLists.Queries.ExportTodos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoLists
{
    [Route("todolists")]
    public class GetById: BaseAsyncEndpoint
        .WithRequest<int>
        .WithoutResponse
    {
        private readonly ISender _sender;

        public GetById(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}"), OpenApiOperation("TodoLists_GetById", "Gets a Todo by ID", "Gets a Todo by ID"), OpenApiTags("Todo")]
        public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken = new CancellationToken())
        {
            var vm = await _sender.Send(new ExportTodosQuery { ListId = id }, cancellationToken);

            return File(vm.Content, vm.ContentType, vm.FileName);
        }
    }
}
