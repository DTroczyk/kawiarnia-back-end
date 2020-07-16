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
                mapper.CreateMap<OrderItem, OrderVm>()
                    .ForMember(dest => dest.coffeeName, x => x.MapFrom(src => src.Coffe.Name))
                    .ForMember(dest => dest.espressoCount, x => x.MapFrom(src => src.EspressoCount))
                    .ForMember(dest => dest.milkCount, x => x.MapFrom(src => src.MilkCount))
                    .ForMember(dest => dest.isContainChocolate, x => x.MapFrom(src => src.IsContainChocolate))
                    //.ForMember(dest => dest.latLng, new float[] = { 0.0, 0.0 })
                    .ForMember(dest => dest.price, x => x.MapFrom(src => src.Price));
                mapper.CreateMap<OrderItem, HistoryVm>()
                    .ForMember(dest => dest.date, x => x.MapFrom(src => src.Order.OrderDate))
                    .ForMember(dest => dest.coffeName, x => x.MapFrom(src => src.Coffe.Name))
                    .ForMember(dest => dest.price, x => x.MapFrom(src => src.Price))
                    //.ForMember(dest => dest.status, null)
                    .ForMember(dest => dest.paymentMethod, x => x.MapFrom(src => src.Order.PaymentMethod));
                mapper.CreateMap<OrderItem, BucketVm>()
                    .ForMember(dest => dest.date, x => x.MapFrom(src => src.Order.OrderDate))
                    .ForMember(dest => dest.coffeName, x => x.MapFrom(src => src.Coffe.Name))
                    .ForMember(dest => dest.price, x => x.MapFrom(src => src.Price))
                    //.ForMember(dest => dest.status, null)
                    .ForMember(dest => dest.paymentMethod, x => x.MapFrom(src => src.Order.PaymentMethod));

            });

            return configurationExpression;
        }
    }
}
