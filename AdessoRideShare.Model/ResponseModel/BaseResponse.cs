using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Model.ResponseModel
{
    public class BaseResponse : IBaseResponse
    {
        public bool IsCompleted { get; set; } = true;
        public string Message { get; set; }
    }

    public interface IBaseResponse
    {
        bool IsCompleted { get; set; }
        string Message { get; set; }
    }
}
