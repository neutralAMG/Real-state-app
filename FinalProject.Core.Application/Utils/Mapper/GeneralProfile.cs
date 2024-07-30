using AutoMapper;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Models.Property;
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


            CreateMap<UpdateUserRequest, UpdateUserModel>()
                .ForMember(dest => dest.role, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<GetUserDto, UserDto>()
                .ReverseMap();

            #endregion

            #region Property mapping setup configuration
            CreateMap<Property, PropertyModel>()
               .ForMember(dest => dest.PropertyPerks, opt => opt.MapFrom(p => p.PropertyPerks))
               .ForMember(dest => dest.PropertyImagesUrls, opt => opt.MapFrom(p => p.PropertyImages))
               .ForMember(dest => dest.PropertyTypeName, opt => opt.MapFrom(p => p.PropertyType.Name))
               .ForMember(dest => dest.SellTypeName, opt => opt.MapFrom(p => p.SellType.Name))
               .ReverseMap();

            CreateMap<Property, SavePropertyModel>()
               .ReverseMap();

            CreateMap<Property, PropertyDto>()
               .ReverseMap();

            #endregion
        }
    }
}
