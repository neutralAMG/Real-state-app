
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PropertyImageRepository : BaseRepository<PropertyImage, int>, IPropertyImageRepository
    {
        private readonly FinalProjectContext _context;

        public PropertyImageRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }



        public override async Task<PropertyImage> SaveAsync(PropertyImage entity)
        {
            return await base.SaveAsync(entity);
        }

        public async Task<bool> UpdateAsync(PropertyImage entity)
        {
            try
            {
                PropertyImage PropertyImageToBeUpdate = await _context.PropertyImages.FindAsync(entity.Id);

                PropertyImageToBeUpdate.ImgUrl = entity.ImgUrl;

                _context.PropertyImages.Attach(PropertyImageToBeUpdate);
                _context.Entry(PropertyImageToBeUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(PropertyImage entity)
        {

            if (!await ExistsAsync(P => P.Id == entity.Id)) return false;

            PropertyImage PropertyToBeDeleted = await _context.PropertyImages.FindAsync(entity.Id);

            return await base.DeleteAsync(PropertyToBeDeleted);
        }

    }
}
