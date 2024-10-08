using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Entities.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.WebsitesContracts.FeatureRepositories;

public interface IFeatureRepository :
                                        IAsyncRepository,
                                        IAsyncInsertableRepository<Feature>,
                                        IAsyncUpdatableRepository<Feature>,
                                        IAsyncDeletableRepository<Feature>,
                                        IAsyncQueryableRepository<Feature>,
                                        IAsyncFindableRepository<Feature>,
                                        IAsyncOrderableRepository<Feature>,
                                        IAsyncTransactionRepository
{
}
