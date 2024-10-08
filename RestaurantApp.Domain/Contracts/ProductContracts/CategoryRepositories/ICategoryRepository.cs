using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.ProductContracts.CategoryRepositories;

public interface ICategoryRepository : 
                                        IAsyncRepository, 
                                        IAsyncInsertableRepository<Category>, 
                                        IAsyncUpdatableRepository<Category>, 
                                        IAsyncDeletableRepository<Category>, 
                                        IAsyncQueryableRepository<Category>, 
                                        IAsyncFindableRepository<Category>, 
                                        IAsyncOrderableRepository<Category>, 
                                        IAsyncTransactionRepository
{
}
