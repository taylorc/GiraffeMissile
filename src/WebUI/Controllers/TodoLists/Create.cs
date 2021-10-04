using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using GiraffeMissile.Application.TodoLists.Commands.CreateTodoList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GiraffeMissile.WebUI.Controllers.TodoLists
{
    [Route("todolists")]
    public class Create: BaseAsyncEndpoint
        .WithRequest<CreateTodoListCommand>
        .WithResponse<int>
    {
        private readonly ISender _sender;

        public Create(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost, OpenApiOperation("TodoLists_Create", "Creates a Todo by ID", "Creates a Todo by ID"), OpenApiTags("TodoLists")]
        public override async Task<ActionResult<int>> HandleAsync(CreateTodoListCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            return await _sender.Send(command, cancellationToken);
        }
    }
}
