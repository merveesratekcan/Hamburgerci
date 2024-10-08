using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.ProductContracts.CampaignRepositories;

public interface ICampaignRepository : 
                                        IAsyncRepository, 
                                        IAsyncInsertableRepository<Campaign>, 
                                        IAsyncUpdatableRepository<Campaign>, 
                                        IAsyncDeletableRepository<Campaign>, 
                                        IAsyncQueryableRepository<Campaign>, 
                                        IAsyncFindableRepository<Campaign>, 
                                        IAsyncOrderableRepository<Campaign>, 
                                        IAsyncTransactionRepository
{
}
