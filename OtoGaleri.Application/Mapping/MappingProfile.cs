using AutoMapper;
using OtoGaleri.Application.DTOs.BrandDtos;
using OtoGaleri.Application.DTOs.CustomerDtos;
using OtoGaleri.Application.DTOs.ModelDtos;
using OtoGaleri.Application.DTOs.SaleDtos;
using OtoGaleri.Application.DTOs.UserDtos;
using OtoGaleri.Application.DTOs.VehicleDtos;
using OtoGaleri.Core.Entities;

namespace OtoGaleri.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Brand, BrandDto>().ReverseMap();
        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();

        CreateMap<Model, ModelDto>().ReverseMap();
        CreateMap<CreateModelDto, Model>();
        CreateMap<UpdateModelDto, Model>();

        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<CreateVehicleDto, Vehicle>();
        CreateMap<UpdateVehicleDto, Vehicle>();

        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();

        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();

        CreateMap<Sale, SaleDto>().ReverseMap();
        CreateMap<CreateSaleDto, Sale>();
        CreateMap<UpdateSaleDto, Sale>();
    }
}