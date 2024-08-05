

using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PropertyTypeRepository : BaseCompleteRepository<PropertyType, int>, IPropertyTypeRepository
    {
        private readonly FinalProjectContext _context;

        public PropertyTypeRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<PropertyType>> GetAllAsync()
        {
            return await _context.PropertyTypes.Include(p => p.Properties).ToListAsync(); 
        }

        public override async Task<PropertyType> GetByIdAsync(int id)
        {
            return await _context.PropertyTypes.Include(p => p.Properties).Where(p => p.Id == id).FirstOrDefaultAsync(); 
        }

        public override async Task<PropertyType> SaveAsync(PropertyType entity)
        {
            if (await ExistsAsync(e => e.Name == entity.Name)) return null;

            return await base.SaveAsync(entity);
        }

        public override async Task<PropertyType> UpdateAsync(PropertyType entity)
        {
            if (!await ExistsAsync(p => p.Id == entity.Id)) return null;

            PropertyType PropertyTypeToBeSaved = await _context.PropertyTypes.FindAsync(entity.Id);

            PropertyTypeToBeSaved.Name = entity.Name;
            PropertyTypeToBeSaved.Description = entity.Description;

            return await base.UpdateAsync(PropertyTypeToBeSaved);
        }

        public override async Task<bool> DeleteAsync(int id)
        {

            if (!await ExistsAsync(P => P.Id == id)) return false;

            bool DeleteOperation = await base.DeleteAsync(id);

            if (DeleteOperation)
            {
                IQueryable<Property> PropertiesToBeDeleted = _context.Properties.Where(p => p.PropertyTypeId == id);
                _context.Properties.RemoveRange(PropertiesToBeDeleted);
                await _context.SaveChangesAsync();
            }
            return DeleteOperation;
        }

    }
}
