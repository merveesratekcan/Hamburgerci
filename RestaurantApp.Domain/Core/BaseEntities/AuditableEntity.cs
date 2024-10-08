using RestaurantApp.Domain.Core.Interfaces;

namespace RestaurantApp.Domain.Core.BaseEntities;

public class AuditableEntity : BaseEntity, IDeletableEntity
{
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
}
