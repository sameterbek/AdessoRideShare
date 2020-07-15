using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdessoRideShare.Api.Authorization;
using AdessoRideShare.Model.RequestModel.UserTravelPlan;
using AdessoRideShare.Model.ResponseModel.UserTravelPlan;
using AdessoRideShare.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdessoRideShare.Api.Controllers
{
    [AuthorizationAttribute]
    [Route("[controller]")]
    [ApiController]
    public class UserTravelController : ControllerBase
    {
        private readonly IUserTravelPlanService _userTravelPlanService;
        public UserTravelController(IUserTravelPlanService userTravelPlanService)
        {
            _userTravelPlanService = userTravelPlanService;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<UserTravelPlanInsertResponse> CreateUserTravelPlan(UserTravelPlanInsertRequest Request)
        {
            return _userTravelPlanService.CreateUserTravelPlan(Request);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<UserTravelPlanChangeStateResponse> TravelPlanChangeState(UserTravelPlanChangeStateRequest Request)
        {
            return _userTravelPlanService.UserTravelPlanChangeState(Request);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<SearchUserTravelPlanResponse> SearchUserTravelPlans(SearchUserTravelPlanRequest Request)
        {
            return _userTravelPlanService.SearchUserTravelPlans(Request);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<TravelPlanJoinResponse> JoinTravelPlan(TravelPlanJoinRequest Request)
        {
            return _userTravelPlanService.JoinTravelPlan(Request);
        }
    }
}
