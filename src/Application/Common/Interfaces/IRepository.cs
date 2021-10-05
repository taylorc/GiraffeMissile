using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using GiraffeMissile.Application.Common.Models;
using Microsoft.Extensions.Primitives;

namespace GiraffeMissile.Application.Common.Interfaces
{
    public interface IRepository<T>: IRepositoryBase<T> where T: class
    {
        Task<PaginatedList<TProjectTo>> PaginatedListAsync<TProjectTo>(Specification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<List<TProjectTo>> ProjectedListAsync<TProjectTo>(Specification<T> specification, CancellationToken cancellationToken);
        //Task<PaginatedList<T>> PaginatedListAsync(Specification<T> specification, CancellationToken cancellationToken);
    }
}
