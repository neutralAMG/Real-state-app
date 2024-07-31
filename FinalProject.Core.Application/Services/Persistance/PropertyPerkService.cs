
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
        private readonly IMapper _mapper;

        public PropertyPerkService(IPropertyPerkRepository propertyPerkRepository, IMapper mapper, string name = "property perk") : base(propertyPerkRepository, mapper, name)
        {
            _propertyPerkRepository = propertyPerkRepository;
            _mapper = mapper;
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

                List<int> propertysPerkids = propertyPerks.Select(p => p.PerkId).ToList();

                List<PropertyPerk> oldPropertyPerksToDelete = propertyPerks.Where(p => propertysPerkids.Contains(p.PerkId) == false).ToList();

                List<int> NewPropertyPerksToSave = perkIds.Where(p => propertysPerkids.Contains(p) == false).ToList();

                if (oldPropertyPerksToDelete.Count > 0)
                {
                    foreach (PropertyPerk propertyPerk in oldPropertyPerksToDelete)
                    {
                        await DeleteAsync(propertyPerk.Id);
                    }

                }

                if (NewPropertyPerksToSave.Count > 0)
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
