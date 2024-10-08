using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.OrdersContracts.CartProductRepositories;
using RestaurantApp.Domain.Entities.Orders;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.OrderRepositories.CartProductRepository;

public class CartProductRepository : EFBaseRepository<CartProduct>, ICartProductRepository
{
    public CartProductRepository(AppDbContext context) : base(context)
    {
    }
}
