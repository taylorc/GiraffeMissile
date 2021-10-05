using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GiraffeMissile.Domain.Entities;

namespace GiraffeMissile.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
    {
        private readonly IRepository<TodoList> _repository;

        public UpdateTodoListCommandHandler(IRepository<TodoList> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.Id);
            }

            entity.Title = request.Title;

            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
