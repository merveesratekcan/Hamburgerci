using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Orders;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.OrdersContracts.CartRepositories;

public interface ICartRepository :
                                        IAsyncRepository,
                                        IAsyncInsertableRepository<Cart>,
                                        IAsyncUpdatableRepository<Cart>,
                                        IAsyncDeletableRepository<Cart>,
                                        IAsyncQueryableRepository<Cart>,
                                        IAsyncFindableRepository<Cart>,
                                        IAsyncOrderableRepository<Cart>,
                                        IAsyncTransactionRepository
{
 
}
