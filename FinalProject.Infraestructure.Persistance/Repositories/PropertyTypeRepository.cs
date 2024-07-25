

using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PropertyTypeRepository : BaseCompleteRepository<PropertyType, int>
    {
        private readonly FinalProjectContext _context;

        public PropertyTypeRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IList<PropertyType>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<PropertyType> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public override async Task<PropertyType> SaveAsync(PropertyType entity)
        {
            return await base.SaveAsync(entity);
        }

        public override async Task<PropertyType> UpdateAsync(PropertyType entity)
        {
            if (!await ExistsAsync(p => p.Id == entity.Id)) return null;
            PropertyType PropertyTypeToBeSaved = await _context.PropertyTypes.FindAsync(entity.Id);

            PropertyTypeToBeSaved.Name = entity.Name;

            return await base.UpdateAsync(PropertyTypeToBeSaved);
        }

        public virtual async Task<bool> DeleteAsync(PropertyType entity)
        {

            if (!await ExistsAsync(P => P.Id == entity.Id)) return false;

            PropertyType PropertyTypeToBeDeleted = await _context.PropertyTypes.FindAsync(entity.Id);

            bool DeleteOperation = await base.DeleteAsync(PropertyTypeToBeDeleted);

            if (DeleteOperation)
            {

                IQueryable<Property> PropertiesToBeDeleted = _context.Properties.Where(p => p.PropertyTypeId == entity.Id);
                _context.Properties.RemoveRange(PropertiesToBeDeleted);
                await _context.SaveChangesAsync();
            }
            return DeleteOperation;
        }

    }
}
