using AdessoRideShare.Db.Entity;
using AdessoRideShare.Db.Enum;
using AdessoRideShare.Model.DataModel.UserTravelPlan;
using AdessoRideShare.Model.RequestModel.Authentication;
using AdessoRideShare.Model.RequestModel.UserTravelPlan;
using AdessoRideShare.Util;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Api.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserTravelPlanInsertRequest, UserTravelPlan>()
                .ForMember(dest => dest.TravelState, opt => opt.MapFrom(src => (int)ETravelState.Publish));

            CreateMap<UserTravelPlan, UserTravelPlanModel>()
                .ForMember(dest => dest.TravelState, opt => opt.MapFrom(src => Converter.GetEnumValue<ETravelState>(src.TravelState).ToString()))
                .ForMember(dest => dest.FromCity, opt => opt.MapFrom(src => src.FromCity.Name))
                .ForMember(dest => dest.ToCity, opt => opt.MapFrom(src => src.ToCity.Name));

            CreateMap<CreateUserRequest, User>();
        }
    }
}
