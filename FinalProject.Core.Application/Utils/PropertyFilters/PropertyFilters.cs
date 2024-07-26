

using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Domain.Entities;


namespace FinalProject.Core.Application.Utils.PropertyFilters
{
    static class PropertyFilters
    {


        public static IEnumerable<Property> FilterProperties(IEnumerable<Property> propertiesToBeFilter, PropertyFilterModel filterModel)
        {

            if (filterModel.MinBathrooms > 0) propertiesToBeFilter = propertiesToBeFilter.Where(p => p.AmountOfBathrooms >= filterModel.MinBathrooms);

            if (filterModel.MaxBathrooms > 0) propertiesToBeFilter = propertiesToBeFilter.Where(p => p.AmountOfBathrooms <= filterModel.MaxBathrooms);

            if (filterModel.MinBedrooms > 0) propertiesToBeFilter = propertiesToBeFilter.Where(p => p.AmountOfBedrooms >= filterModel.MinBedrooms);

            if (filterModel.MaxBedrooms > 0) propertiesToBeFilter = propertiesToBeFilter.Where(p => p.AmountOfBedrooms <= filterModel.MaxBedrooms);

            if (filterModel.MinPrice > 0) propertiesToBeFilter = propertiesToBeFilter.Where(p => p.PropertyPrice >= filterModel.MinPrice);

            if (filterModel.MaxPrice > 0) propertiesToBeFilter = propertiesToBeFilter.Where(p => p.PropertyPrice <= filterModel.MaxPrice);

            if (filterModel.PropertyType > 0) propertiesToBeFilter = propertiesToBeFilter.Where(p => p.PropertyTypeId == filterModel.PropertyType);

            if (filterModel.MinPrice > 0) propertiesToBeFilter = propertiesToBeFilter.Where(p => p.PropertyPrice >= filterModel.MinSize);

            if (filterModel.MaxPrice > 0) propertiesToBeFilter = propertiesToBeFilter.Where(p => p.PropertyPrice >= filterModel.MaxSize);

            return propertiesToBeFilter;
        }


    }
}
