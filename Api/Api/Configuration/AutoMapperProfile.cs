using Api.BLL.Entity;
using Api.ViewModels.ViewModel;
using AutoMapper;
using System;
using System.Linq;

namespace Api.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserVm>()
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
            CreateMap<OrderItem, OrderVm>()
                .ForMember(dest => dest.coffeeName, x => x.MapFrom(src => src.Coffee.Name))
                .ForMember(dest => dest.espressoCount, x => x.MapFrom(src => src.EspressoCount))
                .ForMember(dest => dest.milkCount, x => x.MapFrom(src => src.MilkCount))
                .ForMember(dest => dest.isContainChocolate, x => x.MapFrom(src => src.IsContainChocolate))
                .ForMember(dest => dest.price, x => x.MapFrom(src => src.Price))
                .ForMember(dest => dest.id, x => x.MapFrom(src => src.Id));
            CreateMap<Order, HistoryVm>()
                .ForMember(dest => dest.date, x => x.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.price, x => x.MapFrom(src => Math.Round(src.Items.Sum(y => y.Price), 2)))
                .ForMember(dest => dest.status, x => x.MapFrom(src => src.IsPaymentCompleted))
                .ForMember(dest => dest.paymentMethod, x => x.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.items, x => x.MapFrom(src => src.Items))
                .ForMember(dest => dest.id, x => x.MapFrom(src => src.Id));
            CreateMap<Order, BucketVm>()
                .ForMember(dest => dest.price, x => x.MapFrom(src => Math.Round(src.Items.Sum(y => y.Price), 2)))
                .ForMember(dest => dest.status, x => x.MapFrom(src => src.IsPaymentCompleted))
                .ForMember(dest => dest.items, x => x.MapFrom(src => src.Items))
                .ForMember(dest => dest.id, x => x.MapFrom(src => src.Id));

            CreateMap<HistoryVm, Order>()
                .ForMember(dest => dest.OrderDate, x => x.MapFrom(src => src.date))
                .ForMember(dest => dest.PaymentMethod, x => x.MapFrom(src => src.paymentMethod))
                .ForMember(dest => dest.Items, x => x.MapFrom(src => src.items))
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.id));
            CreateMap<BucketVm, Order>()
                .ForMember(dest => dest.Items, x => x.MapFrom(src => src.items))
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.id));
            CreateMap<UserVm, User>()
                .ForMember(dest => dest.UserName, x => x.MapFrom(src => src.username))
                .ForMember(dest => dest.PasswordHash, x => x.MapFrom(src => src.password))
                .ForMember(dest => dest.Email, x => x.MapFrom(src => src.email))
                .ForMember(dest => dest.FirstName, x => x.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, x => x.MapFrom(src => src.lastName))
                .ForMember(dest => dest.DateOfBirth, x => x.MapFrom(src => src.dateOfBirth))
                .ForMember(dest => dest.Street, x => x.MapFrom(src => src.road))
                .ForMember(dest => dest.HouseNumber, x => x.MapFrom(src => src.houseNumber))
                .ForMember(dest => dest.PostalCode, x => x.MapFrom(src => src.zipcode))
                .ForMember(dest => dest.City, x => x.MapFrom(src => src.place))
                .ForMember(dest => dest.PhoneNumber, x => x.MapFrom(src => src.telephone));
            CreateMap<OrderVm, OrderItem>()
                .ForMember(dest => dest.CoffeeId, x => x.MapFrom(src => src.coffeeName))
                .ForMember(dest => dest.EspressoCount, x => x.MapFrom(src => src.espressoCount))
                .ForMember(dest => dest.MilkCount, x => x.MapFrom(src => src.milkCount))
                .ForMember(dest => dest.IsContainChocolate, x => x.MapFrom(src => src.isContainChocolate))
                .ForMember(dest => dest.Price, x => x.MapFrom(src => src.price))
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.id));
        }
    }
}
