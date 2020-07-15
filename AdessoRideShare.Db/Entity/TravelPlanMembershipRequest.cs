using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Db.Entity
{
    public class TravelPlanMembershipRequest : Base
    {
        public DateTime RequestDate { get; set; }
        public decimal TravelPlanId { get; set; }
        public decimal UserId { get; set; }
        public decimal RequestStatus { get; set; }
    }
}
