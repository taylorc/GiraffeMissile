using System.Collections.Generic;
using Ardalis.Specification.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiraffeMissile.Application.Common.Interfaces;
using GiraffeMissile.Application.Common.Mappings;
using GiraffeMissile.Application.Common.Models;
using GiraffeMissile.Application.TodoLists.Queries.ExportTodos;
using Microsoft.EntityFrameworkCore;

namespace GiraffeMissile.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext DbContext;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            DbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TProjectTo>> PaginatedListAsync<TProjectTo>(Specification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var queryResult = ApplySpecification(specification)
                .ProjectTo<TProjectTo>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(pageNumber, pageSize);

            return await queryResult;
        }

        public async Task<List<TProjectTo>> ProjectedListAsync<TProjectTo>(Specification<T> specification, CancellationToken cancellationToken)
        {
            return await ApplySpecification(specification)
            .ProjectTo<TProjectTo>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
