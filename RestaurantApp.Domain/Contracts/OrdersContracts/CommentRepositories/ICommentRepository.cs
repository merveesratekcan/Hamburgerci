using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Orders;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.OrdersContracts.CommentRepositories;

public interface ICommentRepository :
                                        IAsyncRepository,
                                        IAsyncInsertableRepository<Comment>,
                                        IAsyncUpdatableRepository<Comment>,
                                        IAsyncDeletableRepository<Comment>,
                                        IAsyncQueryableRepository<Comment>,
                                        IAsyncFindableRepository<Comment>,
                                        IAsyncOrderableRepository<Comment>,
                                        IAsyncTransactionRepository
{
}
