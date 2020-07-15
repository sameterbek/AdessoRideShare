using AdessoRideShare.Db.Entity;
using AdessoRideShare.Model.Attribute;
using AdessoRideShare.Model.ResponseModel;
using AdessoRideShare.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdessoRideShare.Api.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            BaseResponse response = new BaseResponse();
            if (actionContext.HttpContext?.User?.Identity != null && actionContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)actionContext.HttpContext.User.Identity;
                Claim claim = identity.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Name);
                if (claim == null)
                {
                    response.IsCompleted = false;
                    response.Message = "Invalid Authorization Token";
                    actionContext.Result = new JsonResult(response);
                }
                else
                {
                    if (actionContext.HttpContext.Request.Method == HttpMethod.Post.Method)
                    {
                        var args = actionContext.ActionArguments?.Values;
                        if (args != null)
                        {

                            var userId = SerializeJson<string>.Deserialize(claim.Value);
                            foreach (var arg in args)
                            {
                                var specialProperties = arg.GetType().GetProperties().Where(pi => pi.GetCustomAttributes<UserControlAttribute>(true).Any());
                                foreach (var property in specialProperties)
                                {
                                    var value = property.GetValue(arg);
                                    if (value.ToString() != userId)
                                    {
                                        response.IsCompleted = false;
                                        response.Message = "Geçersiz işlem.";
                                        actionContext.Result = new JsonResult(response);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    base.OnActionExecuting(actionContext);
                }

            }
            else
            {
                response.IsCompleted = false;
                response.Message = "Invalid Authorization Token";
                actionContext.Result = new JsonResult(response);
            }
        }
    }
}
