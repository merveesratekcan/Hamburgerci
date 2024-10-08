using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.ProductContracts.ProductCampaignRepositories;

public interface IProductCampaignRepository: 
                                                IAsyncRepository,
                                                IAsyncInsertableRepository<ProductCampaign>,
                                                IAsyncUpdatableRepository<ProductCampaign>,
                                                IAsyncDeletableRepository<ProductCampaign>,
                                                IAsyncQueryableRepository<ProductCampaign>,
                                                IAsyncFindableRepository<ProductCampaign>,
                                                IAsyncOrderableRepository<ProductCampaign>,
                                                IAsyncTransactionRepository
{
}

