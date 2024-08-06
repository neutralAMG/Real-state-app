

using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Domain.Entities;
using System.Collections.Generic;


namespace FinalProject.Core.Application.Utils.PropertyFilters
{
    static class PropertyFilters
    {


        public static IEnumerable<Property> FilterProperties(IEnumerable<Property> propertiesToBeFilter, PropertyFilterModel filterModel)
        {
			IEnumerable < Property > propertiesToReturn = propertiesToBeFilter;

			if (filterModel.Bathrooms > 0) propertiesToReturn = propertiesToReturn.Where(p => p.AmountOfBathrooms >= filterModel.Bathrooms);

            if (filterModel.Bedrooms > 0) propertiesToReturn = propertiesToReturn.Where(p => p.AmountOfBedrooms >= filterModel.Bedrooms);

            if (filterModel.MinPrice > 0) propertiesToReturn = propertiesToReturn.Where(p => p.PropertyPrice >= filterModel.MinPrice);

            if (filterModel.MaxPrice > 0) propertiesToReturn = propertiesToReturn.Where(p => p.PropertyPrice <= filterModel.MaxPrice);

            if (filterModel.PropertyType > 0) propertiesToReturn = propertiesToReturn.Where(p => p.PropertyTypeId == filterModel.PropertyType);

            return propertiesToReturn;
        }


    }
}
