using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.Interfaces;

public interface IAsyncFindableRepository<TEntity> where TEntity : BaseEntity
{
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> exception = null);
    Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true);
    //tracking = true ise veri takip edilir.
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> exception, bool tracking = true);
}
