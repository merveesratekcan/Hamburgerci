using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.ProductContracts.ProductRepositories;

public interface IProductRepository : 
    
                                        IAsyncRepository, 
                                        IAsyncInsertableRepository<Product>, 
                                        IAsyncUpdatableRepository<Product>, 
                                        IAsyncDeletableRepository<Product>, 
                                        IAsyncQueryableRepository<Product>, 
                                        IAsyncFindableRepository<Product>, 
                                        IAsyncOrderableRepository<Product>, 
                                        IAsyncTransactionRepository
{
}
