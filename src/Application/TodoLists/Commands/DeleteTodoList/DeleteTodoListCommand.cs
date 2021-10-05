using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GiraffeMissile.Domain.Entities;

namespace GiraffeMissile.Application.TodoLists.Commands.DeleteTodoList
{
    public class DeleteTodoListCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
    {
        private readonly IRepository<TodoList> _repository;

        public DeleteTodoListCommandHandler(IRepository<TodoList> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.Id);
            }

            await _repository.DeleteAsync(entity, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
