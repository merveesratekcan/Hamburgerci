using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Websites;

public class AboutUs : AuditableEntity
{

    public byte[]? ImageUrl { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

}
