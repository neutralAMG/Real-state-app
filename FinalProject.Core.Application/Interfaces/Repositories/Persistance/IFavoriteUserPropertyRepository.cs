﻿using FinalProject.Core.Application.Core;
using FinalProject.Core.Domain.Entities;


namespace FinalProject.Core.Application.Interfaces.Repositories.Persistance
{
    public interface IFavoriteUserPropertyRepository : IBaseRepository<FavoriteUserProperty, Guid>
    {
    }
}