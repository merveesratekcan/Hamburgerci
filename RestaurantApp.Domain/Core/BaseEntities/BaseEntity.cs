using RestaurantApp.Domain.Core.Interfaces;
using RestaurantApp.Domain.Enums;

namespace RestaurantApp.Domain.Core.BaseEntities
{
    public class BaseEntity : IUpdatableEntity
    {
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Status Status { get; set; }
    }
}
