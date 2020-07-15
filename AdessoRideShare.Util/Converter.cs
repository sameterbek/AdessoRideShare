using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Util
{
    public class Converter
    {
        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            T defaultEnum;
            if (Enum.TryParse(intValue.ToString(), true, out defaultEnum))
                if (Enum.IsDefined(typeof(T), defaultEnum) | defaultEnum.ToString().Contains(","))
                    return defaultEnum;
                else
                    throw new Exception($"{intValue} is not a value of the enum");
            else
                throw new Exception($"{intValue} is not a member of the enum");
        }
    }
}
