using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GiraffeMissile.Domain.Entities;
using GiraffeMissile.Domain.Enums;
using Ardalis.Specification;

namespace GiraffeMissile.Application.TodoItems.Commands.UpdateTodoItemDetail
{
    public class UpdateTodoItemDetailCommand : IRequest
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public PriorityLevel Priority { get; set; }

        public string Note { get; set; }
    }

    public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
    {
        private readonly IRepository<TodoItem> _repository;

        public UpdateTodoItemDetailCommandHandler(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.ListId = request.ListId;
            entity.Priority = request.Priority;
            entity.Note = request.Note;

            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
