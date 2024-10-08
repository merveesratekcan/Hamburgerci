using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Entities.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.WebsitesContracts.SocialMediaRepositories;

public interface ISocialMediaRepository :
                                        IAsyncRepository,
                                        IAsyncInsertableRepository<SocialMedia>,
                                        IAsyncUpdatableRepository<SocialMedia>,
                                        IAsyncDeletableRepository<SocialMedia>,
                                        IAsyncQueryableRepository<SocialMedia>,
                                        IAsyncFindableRepository<SocialMedia>,
                                        IAsyncOrderableRepository<SocialMedia>,
                                        IAsyncTransactionRepository
{
}
