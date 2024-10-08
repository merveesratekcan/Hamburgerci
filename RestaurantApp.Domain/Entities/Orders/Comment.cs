using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Orders;

public class Comment : AuditableEntity
{
    public string CommentName { get; set; }
    public string CommentTitle { get; set; }
    public string Comments { get; set; }
    public string? CommentImageUrl { get; set; }
    public bool CommentStatus { get; set; }
}
