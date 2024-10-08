using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Websites;

public class ContactUs : AuditableEntity
{
    public string ContactLocation { get; set; }
    public string ContactNumber { get; set; }
    public string ContactMail { get; set; }
    public string ContactFooterDescription { get; set; }
}
