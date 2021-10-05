using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GiraffeMissile.Domain.Entities;
using Ardalis.Specification;

namespace GiraffeMissile.Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
    {
        private readonly IRepository<TodoItem> _repository;

        public UpdateTodoItemCommandHandler(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.Title = request.Title;
            entity.Done = request.Done;

            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
