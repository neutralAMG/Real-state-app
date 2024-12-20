﻿using AutoMapper;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Features.Account.Commands.RegisterAdminTypeUser;
using FinalProject.Core.Application.Features.Account.Commands.RegisterDeveloperTypeUser;
using FinalProject.Core.Application.Features.Perks.Commands.CreatePerk;
using FinalProject.Core.Application.Features.Perks.Commands.UpdatePerk;
using FinalProject.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType;
using FinalProject.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType;
using FinalProject.Core.Application.Features.SellTypes.Comands.CreateSellType;
using FinalProject.Core.Application.Features.SellTypes.Comands.UpdateSellType;
using FinalProject.Core.Application.Models;
using FinalProject.Core.Application.Models.FavoriteUserProperty;
using FinalProject.Core.Application.Models.Perk;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Models.PropertyImgae;
using FinalProject.Core.Application.Models.PropertyPerk;
using FinalProject.Core.Application.Models.PropertyType;
using FinalProject.Core.Application.Models.SellType;
using FinalProject.Core.Application.Models.User;
using FinalProject.Core.Domain.Entities;


namespace FinalProject.Core.Application.Utils.Mapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region user mappings setup configuration
            CreateMap<GetUserDto, UserModel>()
                .ReverseMap();

            CreateMap<UpdateUserModel, UserModel>()
                .ReverseMap();

            CreateMap<UpdateUserRequest, UpdateUserModel>()
                .ForMember(dest => dest.role, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserModel>()
            .ForMember(dest => dest.role, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<GetUserDto, UserDto>()
                .ReverseMap();

            CreateMap<GetUserStatisticDto, HomeViewStatisticsModel>()
                .ReverseMap();

            CreateMap<UpdateUserRequest, SaveUserModel>()
                .ReverseMap();

            CreateMap<RegisterDeveloperTypeUserCommand, RegisterRequest>()
                .ReverseMap();

            CreateMap<RegisterAdminTypeUserCommand, RegisterRequest>()
                .ReverseMap();
            #endregion

            #region Property mapping setup configuration
            CreateMap<Property, PropertyModel>()
               .ForMember(dest => dest.PropertyPerks, opt => opt.MapFrom(p => p.PropertyPerks.Select(p => p.Perk)))
               .ForMember(dest => dest.PropertyImagesUrls, opt => opt.MapFrom(p => p.PropertyImages))
               .ForMember(dest => dest.PropertyTypeName, opt => opt.MapFrom(p => p.PropertyType.Name))
               .ForMember(dest => dest.SellTypeName, opt => opt.MapFrom(p => p.SellType.Name))
               .ReverseMap()
               .ForMember(dest => dest.PropertyPerks, opt => opt.Ignore())
               .ForMember(dest => dest.PropertyImages, opt => opt.Ignore())
               .ForMember(dest => dest.PropertyType, opt => opt.Ignore())
               .ForMember(dest => dest.SellType, opt => opt.Ignore());


            CreateMap<Property, SavePropertyModel>()

               .ReverseMap()
                 .ForMember(dest => dest.PropertyPerks, opt => opt.Ignore())
                 .ForMember(dest => dest.PropertyImages, opt => opt.Ignore());

            CreateMap<Property, PropertyDto>()
               .ReverseMap();

            #endregion

            #region Perk mapping setup configuration 
            CreateMap<Perk, PerkModel>()
               .ReverseMap()
               .ForMember(dest => dest.PropertyPerks, opt => opt.Ignore());


            CreateMap<Perk, SavePerkModel>()
               .ReverseMap()
               .ForMember(dest => dest.PropertyPerks, opt => opt.Ignore());

            CreateMap<Perk, PerkDto>()
               .ReverseMap()
               .ForMember(dest => dest.PropertyPerks, opt => opt.Ignore());

            CreateMap<Perk, SavePerkDto>()
               .ReverseMap()
               .ForMember(dest => dest.PropertyPerks, opt => opt.Ignore());
            CreateMap<Perk, CreatePerkCommand>()
               .ReverseMap()
               .ForMember(dest => dest.PropertyPerks, opt => opt.Ignore());

            CreateMap<Perk, UpdatePerkCommand>()
              .ReverseMap()
              .ForMember(dest => dest.PropertyPerks, opt => opt.Ignore());

            CreateMap<Perk, UpdatePerkDto>()
            .ReverseMap()
            .ForMember(dest => dest.PropertyPerks, opt => opt.Ignore());
            #endregion


            #region PropertyType mapping setup configuration 
            CreateMap<PropertyType, PropertyTypeModel>()
                .ForMember(dest => dest.AmountOfProperties, opt => opt.MapFrom(p => p.Properties.Count))
              .ReverseMap()
              .ForMember(dest => dest.Properties, opt => opt.Ignore());


            CreateMap<PropertyType, SavePropertyTypeModel>()
              .ReverseMap()
              .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<PropertyType, PropertyTypeDto>()
              .ReverseMap()
              .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<PropertyType, SavePropertyTypeDto>()
             .ReverseMap()
             .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<PropertyType, CreatePropertyTypeCommand>()
             .ReverseMap()
             .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<PropertyType, UpdatePropertyTypeCommand>()
             .ReverseMap()
             .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<PropertyType, UpdatePropertyTypeDto>()
              .ReverseMap()
              .ForMember(dest => dest.Properties, opt => opt.Ignore());
            #endregion

            #region SellType mapping setup configuration
            CreateMap<SellType, SellTypeModel>()
                .ForMember(dest => dest.AmountOfProperties , opt => opt.MapFrom(s => s.Properties.Count))
            .ReverseMap()
            .ForMember(dest => dest.Properties, opt => opt.Ignore());


            CreateMap<SellType, SaveSellTypeModel>()
              .ReverseMap()
              .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<SellType, SellTypeDto>()
              .ReverseMap()
              .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<SellType, SaveSellTypeDto>()
             .ReverseMap()
             .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<SellType, CreateSellTypeCommand>()
             .ReverseMap()
             .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<SellType, UpdateSellTypeCommand>()
             .ReverseMap()
             .ForMember(dest => dest.Properties, opt => opt.Ignore());

            CreateMap<SellType, UpdateSellTypeDto>()
            .ReverseMap()
            .ForMember(dest => dest.Properties, opt => opt.Ignore());
            #endregion


            #region PropertyImage mapping setup configuration 
            CreateMap<PropertyImage, PropertyImageModel>()
              .ReverseMap()
              .ForMember(dest => dest.Property, opt => opt.Ignore());


            CreateMap<PropertyImage, SavePropertyImageModel>()
              .ReverseMap()
              .ForMember(dest => dest.Property, opt => opt.Ignore());

            #endregion

            #region PropertyPerk mapping setup configuration


            CreateMap<PropertyPerk, SavePropertyPerkModel>()
             .ReverseMap()
             .ForMember(dest => dest.Property, opt => opt.Ignore());

            #endregion

            #region FavoriteUserProperty mappings setup configuration
            CreateMap<FavoriteUserProperty, SaveFavoriteUserPropertyModel>()
          .ReverseMap()
          .ForMember(dest => dest.Property, opt => opt.Ignore());
            #endregion
        }
    }
}
