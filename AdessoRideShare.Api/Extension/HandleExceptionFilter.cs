using AdessoRideShare.Model.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Api.Extension
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            BaseResponse response = new BaseResponse();
            var msg = context.Exception.GetBaseException().Message;

            response.Message = msg;
            response.IsCompleted = false;

            // always return a JSON result
            context.Result = new JsonResult(response);

        }
    }
}
