

using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Domain.Entities;
using FinalProject.Core.Application.Enums;
using System.Reflection;

namespace FinalProject.Core.Application.Utils.PropertyFilters
{
    static class PropertyFilters
    {
        public static readonly Dictionary<string, Func<IEnumerable<Property>, int, IEnumerable<Property>>> Filters = new()
        {
            {FilterNames.MinBathrooms.ToString(), FilterByMinBathrooms },
            {FilterNames.MaxBathrooms.ToString(), FilterByMaxBathrooms },
            {FilterNames.MinBedrooms.ToString(), FilterByMinBedrooms },
            {FilterNames.MaxBedrooms.ToString(), FilterByMaxBedrooms },
            {FilterNames.MinPrice.ToString(), FilterByMinPrice },
            {FilterNames.MaxPrice.ToString(), FilterByMaxPrice },
            {FilterNames.PropertyType.ToString(), FilterByPropertyType },
            {FilterNames.MinSize.ToString(), FilterByMinSize },
            {FilterNames.MaxSize.ToString(), FilterByMaxSize },
        };

        public static IEnumerable<Property> FilterProperties(IEnumerable<Property> propertiesToBeFilter, PropertyFilterModel filterModel)
        {
            try
            {
                Type type = filterModel.GetType();

                PropertyInfo[] filterProperties = type.GetProperties();

                foreach (PropertyInfo property in filterProperties)
                {
                    object propertyValue = property.GetValue(filterModel);

                    Func<IEnumerable<Property>, int, IEnumerable<Property>> filterFunc;

                    if (propertyValue is int value && value > 0 && Filters.TryGetValue(property.Name, out filterFunc))
                    {
                        propertiesToBeFilter = filterFunc(propertiesToBeFilter, value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Error will doing the filtering, check the filters name" + ex.Message);
            }
            return propertiesToBeFilter;
        }

        #region Filters
        private static IEnumerable<Property> FilterByMinBathrooms(IEnumerable<Property> propertyList, int filterValue)
        {
            return propertyList.Where(p => p.AmountOfBathrooms >= filterValue);
        }

        private static IEnumerable<Property> FilterByMaxBathrooms(IEnumerable<Property> propertyList, int filterValue)
        {
            return propertyList.Where(p => p.AmountOfBathrooms <= filterValue);
        }

        private static IEnumerable<Property> FilterByMinBedrooms(IEnumerable<Property> propertyList, int filterValue)
        {
            return propertyList.Where(p => p.AmountOfBedrooms >= filterValue);
        }

        private static IEnumerable<Property> FilterByMaxBedrooms(IEnumerable<Property> propertyList, int filterValue)
        {
            return propertyList.Where(p => p.AmountOfBedrooms <= filterValue);
        }

        private static IEnumerable<Property> FilterByMinPrice(IEnumerable<Property> propertyList, int filterValue)
        {
            return propertyList.Where(p => p.PropertyPrice >= filterValue);
        }

        private static IEnumerable<Property> FilterByMaxPrice(IEnumerable<Property> propertyList, int filterValue)
        {
            return propertyList.Where(p => p.PropertyPrice <= filterValue);
        }

        private static IEnumerable<Property> FilterByPropertyType(IEnumerable<Property> propertyList, int filterValue)
        {
            return propertyList.Where(p => p.PropertyTypeId == filterValue);
        }

        private static IEnumerable<Property> FilterByMinSize(IEnumerable<Property> propertyList, int filterValue)
        {
            return propertyList.Where(p => p.SizeInMeters >= filterValue);
        }

        private static IEnumerable<Property> FilterByMaxSize(IEnumerable<Property> propertyList, int filterValue)
        {
            return propertyList.Where(p => p.SizeInMeters <= filterValue);
        }
        #endregion
    }
}
