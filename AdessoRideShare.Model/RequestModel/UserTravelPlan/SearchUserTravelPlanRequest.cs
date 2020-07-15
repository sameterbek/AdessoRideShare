using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Model.RequestModel.UserTravelPlan
{
    public class SearchUserTravelPlanRequest
    {
        public decimal UserId { get; set; }
        public decimal FromCityId { get; set; }
        public decimal ToCityId { get; set; }
    }
}
