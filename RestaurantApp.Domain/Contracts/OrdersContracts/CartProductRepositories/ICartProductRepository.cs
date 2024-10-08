using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Orders;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.OrdersContracts.CartProductRepositories;

public interface ICartProductRepository :
                                        IAsyncRepository,
                                        IAsyncInsertableRepository<CartProduct>,
                                        IAsyncUpdatableRepository<CartProduct>,
                                        IAsyncDeletableRepository<CartProduct>,
                                        IAsyncQueryableRepository<CartProduct>,
                                        IAsyncFindableRepository<CartProduct>,
                                        IAsyncOrderableRepository<CartProduct>,
                                        IAsyncTransactionRepository
{
}
