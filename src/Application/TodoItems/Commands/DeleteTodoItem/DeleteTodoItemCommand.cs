using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using GiraffeMissile.Domain.Entities;

namespace GiraffeMissile.Application.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly IRepository<TodoItem> _repository;

        public DeleteTodoItemCommandHandler(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            await _repository.DeleteAsync(entity, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
