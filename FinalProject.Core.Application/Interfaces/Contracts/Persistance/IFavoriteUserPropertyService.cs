

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.FavoriteUserProperty;
using FinalProject.Core.Domain.Entities;
using System.Security.Cryptography;

namespace FinalProject.Core.Application.Interfaces.Contracts.Persistance
{
    internal interface IFavoriteUserPropertyService : IBaseService<SaveFavoriteUserPropertyModel, FavoriteUserProperty, int>
    {
       
    }
}
