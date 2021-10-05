using GiraffeMissile.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GiraffeMissile.Domain.Entities;

namespace GiraffeMissile.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommand : IRequest<int>
    {
        public string Title { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
    {
        private readonly IRepository<TodoList> _repo;

        public CreateTodoListCommandHandler(IRepository<TodoList> repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList
            {
                Title = request.Title
            };

            await _repo.AddAsync(entity, cancellationToken);

            await _repo.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
