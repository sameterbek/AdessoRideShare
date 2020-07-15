using AdessoRideShare.Db.Entity;
using AdessoRideShare.Db.Enum;
using AdessoRideShare.Model.DataModel.UserTravelPlan;
using AdessoRideShare.Model.RequestModel.UserTravelPlan;
using AdessoRideShare.Model.ResponseModel.UserTravelPlan;
using AdessoRideShare.Repository.Interfaces;
using AdessoRideShare.Service.Interfaces;
using AdessoRideShare.Util;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdessoRideShare.Service.Services
{
    public class TravelPlanMembershipRequestService : BaseService<TravelPlanMembershipRequest>, ITravelPlanMembershipRequestService
    {
        public TravelPlanMembershipRequestService(IRepository<TravelPlanMembershipRequest> repository, IUnitOfWork unitOfWork
            , IMapper mapper) : base(repository, unitOfWork)
        {

        }

    }
}
