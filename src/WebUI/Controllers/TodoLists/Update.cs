using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GiraffeMissile.WebUI.Controllers.TodoLists
{
    [Route("todolists")]
    public class Update: BaseAsyncEndpoint
        .WithRequest<UpdateTodoListDto>
        .WithoutResponse
    {
        private readonly ISender _sender;

        public Update(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("{id}"), OpenApiOperation("TodoLists_Update", "Updates a Todo by ID", "Updates a Todo by ID"), OpenApiTags("Todo")]
        public override async Task<ActionResult> HandleAsync([FromRoute]UpdateTodoListDto request, CancellationToken cancellationToken = new CancellationToken())
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
