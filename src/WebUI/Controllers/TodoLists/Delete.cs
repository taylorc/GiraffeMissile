using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using GiraffeMissile.Application.TodoLists.Commands.DeleteTodoList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace GiraffeMissile.WebUI.Controllers.TodoLists
{
    [Route("todolists")]
    public class Delete: BaseAsyncEndpoint
        .WithRequest<int>
        .WithoutResponse
    {
        private readonly ISender _sender;

        public Delete(ISender sender)
        {
            _sender = sender;
        }

        [HttpDelete("{id}"), OpenApiOperation("TodoLists_Delete", "Deletes a Todo by ID", "Deletes a Todo by ID"), OpenApiTags("TodoLists")]
        public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken = new CancellationToken())
        {
            await _sender.Send(new DeleteTodoListCommand { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
