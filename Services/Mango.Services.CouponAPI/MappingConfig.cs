using AutoMapper;
using Microservices.Services.CouponAPI.Models;
using Microservices.Services.CouponAPI.Models.DTO;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Coupon, CouponDTO>().ReverseMap();
        });

        return mappingConfig;
    }
}