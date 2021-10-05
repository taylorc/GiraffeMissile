using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiraffeMissile.Application.Common.Interfaces;
using GiraffeMissile.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GiraffeMissile.Application.sp;
using GiraffeMissile.Application.Specifications;

namespace GiraffeMissile.Application.TodoLists.Queries.ExportTodos
{
    public class ExportTodosQuery : IRequest<ExportTodosVm>
    {
        public int ListId { get; set; }
    }

    public class ExportTodosQueryHandler : IRequestHandler<ExportTodosQuery, ExportTodosVm>
    {
        private readonly IRepository<TodoItem> _repository;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportTodosQueryHandler(IRepository<TodoItem> repository, IMapper mapper, ICsvFileBuilder fileBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<ExportTodosVm> Handle(ExportTodosQuery request, CancellationToken cancellationToken)
        {
            var vm = new ExportTodosVm();

            var records =
                await _repository.ProjectedListAsync<TodoItemRecord>(new TodoItemByListIdSpecification(request.ListId),
                    cancellationToken);
                    

            vm.Content = _fileBuilder.BuildTodoItemsFile(records);
            vm.ContentType = "text/csv";
            vm.FileName = "TodoItems.csv";

            return await Task.FromResult(vm);
        }
    }
}
