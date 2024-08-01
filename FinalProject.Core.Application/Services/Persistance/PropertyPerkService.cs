
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.PropertyPerk;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Services.Persistance
{
    internal class PropertyPerkService : BaseService<SavePropertyPerkModel, PropertyPerk, int>, IPropertyPerkService
    {
        private readonly IPropertyPerkRepository _propertyPerkRepository;
    
        public PropertyPerkService(IPropertyPerkRepository propertyPerkRepository, IMapper mapper) : base(propertyPerkRepository, mapper, "property perk")
        {
            _propertyPerkRepository = propertyPerkRepository;

        }
        public override Task<Result<SavePropertyPerkModel>> SaveAsync(SavePropertyPerkModel saveModel)
        {
            return base.SaveAsync(saveModel);
        }

        public override Task<Result> DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<Result> UpdateAsync(List<int> perkIds, Guid propertyId)
        {
            Result result = new();
            try
            {
                List<PropertyPerk> propertyPerks = await _propertyPerkRepository.GetAllPerksIdByPropertyId(propertyId);

                List<int> CurrentpropertysPerkids = propertyPerks.Select(p => p.PerkId).ToList();

                List<int> oldPropertyPerksToDelete = CurrentpropertysPerkids.Except(perkIds).ToList();

                List<int> NewPropertyPerksToSave = perkIds.Except(CurrentpropertysPerkids).ToList();

                if (oldPropertyPerksToDelete.Any())
                {
                    foreach (int propertyPerk in oldPropertyPerksToDelete)
                    {
                        await DeleteAsync(propertyPerk);
                    }

                }

                if (NewPropertyPerksToSave.Any())
                {
                    foreach (int p in NewPropertyPerksToSave)
                    {
                        await SaveAsync(new SavePropertyPerkModel
                        {
                            PerkId = p,
                            PropertyId = propertyId,

                        });

                    }
                }
                result.Message = "Property Perks where updated succesfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Chritical error updating the property perks for this property";
                return result;
            }
        }
    }
}
