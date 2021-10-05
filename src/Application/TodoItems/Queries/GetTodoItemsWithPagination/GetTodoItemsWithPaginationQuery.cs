using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiraffeMissile.Application.Common.Interfaces;
using GiraffeMissile.Application.Common.Mappings;
using GiraffeMissile.Application.Common.Models;
using GiraffeMissile.Application.Specifications;
using GiraffeMissile.Domain.Entities;
using MediatR;

namespace GiraffeMissile.Application.TodoItems.Queries.GetTodoItemsWithPagination
{
    public class GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
    {
        public int ListId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
    {
        private readonly IRepository<TodoItem> _repository;

        public GetTodoItemsWithPaginationQueryHandler(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PaginatedListAsync<TodoItemBriefDto>(
                new TodoItemsByListIdOrderedByTitleAscSpecification(request.ListId), request.PageNumber,
                request.PageSize, cancellationToken);
        }
    }
}
