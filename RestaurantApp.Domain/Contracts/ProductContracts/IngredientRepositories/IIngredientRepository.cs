using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.ProductContracts.IngredientRepositories;

public interface IIngredientRepository : 
                                            IAsyncRepository, 
                                            IAsyncInsertableRepository<Ingredient>, 
                                            IAsyncUpdatableRepository<Ingredient>, 
                                            IAsyncDeletableRepository<Ingredient>, 
                                            IAsyncQueryableRepository<Ingredient>, 
                                            IAsyncFindableRepository<Ingredient>, 
                                            IAsyncOrderableRepository<Ingredient>, 
                                            IAsyncTransactionRepository
{
}
