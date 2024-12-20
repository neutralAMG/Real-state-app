﻿using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models.PropertyType;
using FinalProject.Core.Application.Models.SellType;
using FinalProject.Presentation.WebApp.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Presentation.WebApp.Utils.WebappSelectListGenerator
{
    public class SelectListGenerator : ISelectListGenerator
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellTypeService _sellTypeService;

        public SelectListGenerator(IPropertyTypeService propertyTypeService, ISellTypeService sellTypeService)
        {
            _propertyTypeService = propertyTypeService;
            _sellTypeService = sellTypeService;
        }
        public async Task<List<SelectListItem>> GeneratePropertyTypesSelectListAsync(string savedIDFromPropertyToUpdate = null)
        {
            Result<List<PropertyTypeModel>> propertyTypeResult = await _propertyTypeService.GetAllAsync();

            return propertyTypeResult.Data.Select(p => new SelectListItem
            {
               Value = p.Id.ToString(),
               Text = p.Name,
               Selected = savedIDFromPropertyToUpdate !=null && p.Name == savedIDFromPropertyToUpdate,
            }).ToList();
        }

        public async Task<List<SelectListItem>> GenerateSellTypesSelectListAsync(string savedIDFromPropertyToUpdate = null)
        {
            Result<List<SellTypeModel>> sellTypeResult = await _sellTypeService.GetAllAsync();

            return sellTypeResult.Data.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name,
                Selected = savedIDFromPropertyToUpdate !=null && p.Name == savedIDFromPropertyToUpdate,
            }).ToList();
        }
    }
}
