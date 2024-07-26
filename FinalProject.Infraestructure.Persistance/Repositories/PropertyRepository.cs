﻿using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PropertyRepository : BaseCompleteRepository<Property, Guid>, IPropertyRepository
    {
        private readonly FinalProjectContext _context;

        public PropertyRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IList<Property>> GetAllAsync()
        {
            return await _context.Properties.Include(p => p.PropertyImages)
                .Include(p => p.PropertyPerks)
                .Include(p => p.PropertyType)
                .Include(p => p.SellType).ToListAsync();
        }

        public override async Task<Property> GetByIdAsync(Guid id)
        {
            return await _context.Properties.Include(p => p.PropertyImages)
                .Include(p => p.PropertyPerks)
                .Include(p => p.PropertyType)
                .Include(p => p.SellType).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public override async Task<Property> SaveAsync(Property entity)
        {
            return await base.SaveAsync(entity);
        }

        public override async Task<Property> UpdateAsync(Property entity)
        {
            if (!await ExistsAsync(p => p.Id == entity.Id)) return null;

            Property PropertyToBeSaved = await _context.Properties.FindAsync(entity.Id);

            PropertyToBeSaved.SizeInMeters = entity.SizeInMeters;

            PropertyToBeSaved.AmountOfBathrooms = entity.AmountOfBathrooms;

            PropertyToBeSaved.AmountOfBedrooms = entity.AmountOfBedrooms;

            PropertyToBeSaved.PropertyPrice = entity.PropertyPrice;

            PropertyToBeSaved.Description = entity.Description;

            return await base.UpdateAsync(PropertyToBeSaved);
        }

        public virtual async Task<bool> DeleteAsync(Property entity)
        {

            if (!await ExistsAsync(P => P.Id == entity.Id)) return false;

            Property PropertyToBeDeleted = await _context.Properties.FindAsync(entity.Id);

            return await base.DeleteAsync(PropertyToBeDeleted);
        }
        public async Task<List<Property>> GetAllCurrentAgentUserPropertiesAsync(string id)
        {
            if (!await ExistsAsync(P => P.AgentId == id)) return null;

            return await _context.Properties.Include(p => p.PropertyImages)
            .Include(p => p.PropertyPerks)
            .Include(p => p.PropertyType)
            .Include(p => p.SellType).Where(p => p.AgentId == id).ToListAsync();
        }

        public async Task<List<Property>> GetAllCurrentClientUserFavPropertiesAsync(string id)
        {
            if (!await ExistsAsync(P => P.FavoriteUsersProperties.Any( p => p.UserId == id))) return null;

            return await _context.Properties.Include(p => p.PropertyImages)
            .Include(p => p.PropertyPerks)
            .Include(p => p.PropertyType)
            .Include(p => p.SellType).Where(p => p.FavoriteUsersProperties.Any(p => p.UserId == id)).ToListAsync();
        }
    }
}