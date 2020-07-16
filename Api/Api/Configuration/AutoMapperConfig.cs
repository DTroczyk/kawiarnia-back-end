using Api.BLL.Entity;
using Api.BLL.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static IMapperConfigurationExpression Mapping(this IMapperConfigurationExpression configurationExpression)
        {
            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<User, UserVm>()
                    .ForMember(dest => dest.username, x => x.MapFrom(src => src.UserName))
                    .ForMember(dest => dest.password, x => x.MapFrom(src => src.PasswordHash))
                    .ForMember(dest => dest.email, x => x.MapFrom(src => src.Email))
                    .ForMember(dest => dest.firstName, x => x.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.lastName, x => x.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.dateOfBirth, x => x.MapFrom(src => src.DateOfBirth))
                    .ForMember(dest => dest.road, x => x.MapFrom(src => src.Street))
                    .ForMember(dest => dest.houseNumber, x => x.MapFrom(src => src.HouseNumber))
                    .ForMember(dest => dest.zipcode, x => x.MapFrom(src => src.PostalCode))
                    .ForMember(dest => dest.place, x => x.MapFrom(src => src.City))
                    .ForMember(dest => dest.telephone, x => x.MapFrom(src => src.PhoneNumber));
            });

            return configurationExpression;
        }
    }
}
