using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdessoRideShare.Db.Entity
{
    public class TravelPlanMembership : Base
    {
        
        public decimal TravelPlanId { get; set; }
        public decimal UserId { get; set; }

        //[ForeignKey("TravelPlanId")]
        //public virtual UserTravelPlan UserTravelPlan { get; set; }
    }
}
