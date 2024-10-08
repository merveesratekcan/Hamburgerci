using RestaurantApp.Domain.Core.BaseEntities;
using RestaurantApp.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Products
{
    public class Product:AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }
        //nav prop
        public Guid CategoryId { get; set; }
        public virtual Category Categories { get; set; }
        // virtual olarak tanımlanması EFnin Lazy Loading özelliğini kullanmasına izin verir.
        public virtual IEnumerable<ProductCampaign> ProductCampaigns { get; set; }
        public virtual IEnumerable<ProductIngredient> ProductIngredients { get; set; }
        public virtual IEnumerable<CartProduct> CartProducts { get; set; }

        // Koleksiyonları başlatmak için kullanılır, null referans hatalarını önler ve koleksiyonların her zaman geçerli bir örneğe sahip olmasını sağlar.
        public Product()
        {
            ProductIngredients = new HashSet<ProductIngredient>();
            ProductCampaigns = new HashSet<ProductCampaign>();
            CartProducts = new HashSet<CartProduct>();

        }
    }
}
