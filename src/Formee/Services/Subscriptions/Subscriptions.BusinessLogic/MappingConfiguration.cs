using AutoMapper;
using Subscriptions.BusinessLogic.Dtos.Orders;
using Subscriptions.BusinessLogic.Dtos.Subscriptions;
using Subscriptions.BusinessLogic.Dtos.Users;
using Subscriptions.BusinessLogic.Models.Orders;
using Subscriptions.BusinessLogic.Models.Subscriptions;
using Subscriptions.BusinessLogic.Models.Users;

namespace Subscriptions.BusinessLogic;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        //Subscriptions
        CreateMap<SubscriptionModel, SubscriptionDto>()
            .ReverseMap();

        CreateMap<SubscriptionFeatureModel, SubscriptionFeatureDto>()
            .ReverseMap();

        //Orders
        CreateMap<OrderHeaderModel, OrderHeaderDto>()
            .ReverseMap();

        CreateMap<OrderDetailsModel, OrderDetailsDto>()
            .ReverseMap();
        
        CreateMap<CouponModel, CouponDto>()
            .ReverseMap();

        //Users
        CreateMap<UserModel, UserDto>()
            .ReverseMap();

        CreateMap<UserSubscriptionModel, UserSubscriptionDto>()
            .ReverseMap();
    }
}
