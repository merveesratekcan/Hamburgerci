using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Entities.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.WebsitesContracts.ContactUsRepositories;

public interface IContactUsRepository :
                                        IAsyncRepository,
                                        IAsyncInsertableRepository<ContactUs>,
                                        IAsyncUpdatableRepository<ContactUs>,
                                        IAsyncDeletableRepository<ContactUs>,
                                        IAsyncQueryableRepository<ContactUs>,
                                        IAsyncFindableRepository<ContactUs>,
                                        IAsyncOrderableRepository<ContactUs>,
                                        IAsyncTransactionRepository
{
}
