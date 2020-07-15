using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Util
{
    public static class SerializeJson<T>
    {

        public static T Deserialize(string value)
        {
            T result = default(T);
            try
            {
                result = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static string Serialize(T value)
        {
            string result = "";
            try
            {
                result = JsonConvert.SerializeObject(value);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
