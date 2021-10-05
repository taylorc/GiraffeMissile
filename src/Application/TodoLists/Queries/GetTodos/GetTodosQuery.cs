using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiraffeMissile.Application.Common.Interfaces;
using GiraffeMissile.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GiraffeMissile.Application.Specifications;
using GiraffeMissile.Domain.Enums;
using GiraffeMissile.Domain.Entities;

namespace GiraffeMissile.Application.TodoLists.Queries.GetTodos
{
    public class GetTodosQuery : IRequest<TodosVm>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IRepository<TodoList> _repository;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IRepository<TodoList> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return new TodosVm
            {
                PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                    .Cast<PriorityLevel>()
                    .Select(p => new PriorityLevelDto {Value = (int) p, Name = p.ToString()})
                    .ToList(),

                Lists = await _repository.ProjectedListAsync<TodoListDto>(new TodoListOrderedByTitleAscSpecification(),
                    cancellationToken)

            };
        }
    }
}
