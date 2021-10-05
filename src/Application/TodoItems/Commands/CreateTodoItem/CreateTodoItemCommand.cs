using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using GiraffeMissile.Application.Common.Interfaces;
using GiraffeMissile.Domain.Entities;
using GiraffeMissile.Domain.Events;

namespace GiraffeMissile.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public int ListId { get; set; }

        public string Title { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly IRepository<TodoItem> _repository;

        public CreateTodoItemCommandHandler(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                ListId = request.ListId,
                Title = request.Title,
                Done = false
            };

            entity.DomainEvents.Add(new TodoItemCreatedEvent(entity));

            await _repository.AddAsync(entity, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
