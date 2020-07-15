using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Model.DataModel.UserTravelPlan
{
    public class UserTravelPlanModel
    {
        public decimal RecordId { get; set; }
        public string Description { get; set; }
        public decimal Capacity { get; set; }
        public DateTime TravelDate { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string TravelState { get; set; }
    }
}
