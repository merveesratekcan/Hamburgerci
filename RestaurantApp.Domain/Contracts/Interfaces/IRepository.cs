using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.Interfaces;

public interface IRepository
{
    int SaveChanges();
}
