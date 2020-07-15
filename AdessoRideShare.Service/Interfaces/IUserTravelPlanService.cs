using AdessoRideShare.Db.Entity;
using AdessoRideShare.Model.RequestModel.UserTravelPlan;
using AdessoRideShare.Model.ResponseModel.UserTravelPlan;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Service.Interfaces
{
    public interface IUserTravelPlanService : IBaseService<UserTravelPlan>
    {
        UserTravelPlanInsertResponse CreateUserTravelPlan(UserTravelPlanInsertRequest requestModel);

        UserTravelPlanChangeStateResponse UserTravelPlanChangeState(UserTravelPlanChangeStateRequest requestModel);

        SearchUserTravelPlanResponse SearchUserTravelPlans(SearchUserTravelPlanRequest requestModel);

        TravelPlanJoinResponse JoinTravelPlan(TravelPlanJoinRequest requestModel);
    }
}
