using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.WebsitesContracts.SliderRepositories;

public interface ISliderRepository :
                                        IAsyncRepository,
                                        IAsyncInsertableRepository<Slider>,
                                        IAsyncUpdatableRepository<Slider>,
                                        IAsyncDeletableRepository<Slider>,
                                        IAsyncQueryableRepository<Slider>,
                                        IAsyncFindableRepository<Slider>,
                                        IAsyncOrderableRepository<Slider>,
                                        IAsyncTransactionRepository
{
}
