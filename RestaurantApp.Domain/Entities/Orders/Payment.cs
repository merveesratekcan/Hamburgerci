using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Orders;

public class Payment : AuditableEntity
{
    public decimal TotalAmount { get; set; }
}
