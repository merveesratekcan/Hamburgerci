using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Orders;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.OrdersContracts.OrderRepositories;

public interface IOrderRepository :
                                        IAsyncRepository,
                                        IAsyncInsertableRepository<Order>,
                                        IAsyncUpdatableRepository<Order>,
                                        IAsyncDeletableRepository<Order>,
                                        IAsyncQueryableRepository<Order>,
                                        IAsyncFindableRepository<Order>,
                                        IAsyncOrderableRepository<Order>,
                                        IAsyncTransactionRepository
{
}
