using AdessoRideShare.Model.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Model.RequestModel.UserTravelPlan
{
    public class UserTravelPlanInsertRequest
    {
        public string Description { get; set; }
        public decimal Capacity { get; set; }
        public DateTime TravelDate { get; set; }
        [UserControl]
        public decimal UserId { get; set; }
        public decimal FromCityId { get; set; }
        public decimal ToCityId { get; set; }
    }
}
