using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Entities.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.WebsitesContracts.AboutUsRepositories;

public interface IAboutUsRepository :
                                        IAsyncRepository,
                                        IAsyncInsertableRepository<AboutUs>,
                                        IAsyncUpdatableRepository<AboutUs>,
                                        IAsyncDeletableRepository<AboutUs>,
                                        IAsyncQueryableRepository<AboutUs>,
                                        IAsyncFindableRepository<AboutUs>,
                                        IAsyncOrderableRepository<AboutUs>,
                                        IAsyncTransactionRepository
{
}
