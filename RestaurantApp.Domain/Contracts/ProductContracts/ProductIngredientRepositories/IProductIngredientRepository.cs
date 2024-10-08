using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.ProductContracts.ProductIngredientRepositories;

public interface IProductIngredientRepository : 
                                                    IAsyncRepository, 
                                                    IAsyncInsertableRepository<ProductIngredient>, 
                                                    IAsyncUpdatableRepository<ProductIngredient>, 
                                                    IAsyncDeletableRepository<ProductIngredient>, 
                                                    IAsyncQueryableRepository<ProductIngredient>, 
                                                    IAsyncFindableRepository<ProductIngredient>, 
                                                    IAsyncOrderableRepository<ProductIngredient>, 
                                                    IAsyncTransactionRepository
{
    //Ara tablolara doğrudan erişim gerekiyorsa, bu tablolar için repository sınıfları yazabilirsiniz.
    //Ara tablolara repository yazmak, özellikle özel sorgular veya işlemler yapmak gerektiğinde yararlıdır.
    //Repository sınıfları, veri erişim kodunu soyutlayarak daha düzenli ve yönetilebilir hale getirir.
}
