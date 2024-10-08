using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Websites;

public class SocialMedia : AuditableEntity
{
    public string SocialMediaTitle { get; set; }
    public string SocialMediaUrl { get; set; }
    public string SocialMediaIcon { get; set; }
}
