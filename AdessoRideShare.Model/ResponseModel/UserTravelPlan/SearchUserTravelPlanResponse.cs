using AdessoRideShare.Model.DataModel.UserTravelPlan;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Model.ResponseModel.UserTravelPlan
{
    public class SearchUserTravelPlanResponse : BaseResponse
    {
        public List<UserTravelPlanModel> UserTravelPlans { get; set; }
        public List<UserTravelPlanModel> AdditionalTravelPlans { get; set; }
        public SearchUserTravelPlanResponse()
        {
            UserTravelPlans = new List<UserTravelPlanModel>();
            AdditionalTravelPlans = new List<UserTravelPlanModel>();
        }
    }
}
