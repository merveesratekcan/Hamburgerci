﻿using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.Interfaces;

public interface IAsyncUpdatableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task<TEntity> UpdateAsync(TEntity entity);

}

