using AdessoRideShare.Model.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Model.RequestModel.UserTravelPlan
{
    public class TravelPlanJoinRequest
    {
        [UserControl]
        public decimal UserId { get; set; }
        public decimal TravelPlanId { get; set; }
    }
}
