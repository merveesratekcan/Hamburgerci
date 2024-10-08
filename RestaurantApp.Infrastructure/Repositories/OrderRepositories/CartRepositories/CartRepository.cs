using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.OrdersContracts.CartRepositories;
using RestaurantApp.Domain.Entities.Orders;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.OrderRepositories.CartRepositories;

public class CartRepository : EFBaseRepository<Cart>, ICartRepository
{


    public CartRepository(AppDbContext context) : base(context)
    {
  
    }


}
