using GiraffeMissile.Application.Common.Interfaces;
using GiraffeMissile.Application.Common.Security;
using GiraffeMissile.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GiraffeMissile.Application.TodoLists.Commands.PurgeTodoLists
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Policy = "CanPurge")]
    public class PurgeTodoListsCommand : IRequest
    {
    }

    public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
    {
        private readonly IRepository<TodoList> _repository;

        public PurgeTodoListsCommandHandler(IRepository<TodoList> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteRangeAsync(await _repository.ListAsync(cancellationToken), cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
