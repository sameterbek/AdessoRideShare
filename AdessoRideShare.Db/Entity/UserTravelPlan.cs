using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdessoRideShare.Db.Entity
{
    public class UserTravelPlan : Base
    {
        public string Description { get; set; }
        public decimal Capacity { get; set; }
        public DateTime TravelDate { get; set; }
        public decimal UserId { get; set; }

        [ForeignKey("FromCity")]
        public decimal FromCityId { get; set; }

        public decimal ToCityId { get; set; }

        public int TravelState { get; set; }

        
        public virtual City FromCity { get; set; }

        [ForeignKey("ToCityId")]
        public virtual City ToCity { get; set; }

        [ForeignKey("TravelPlanId")]
        public virtual List<TravelPlanMembership> TravelPlanMemberships { get; set; }
    }
}
