using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Db.Entity
{
    public class City : Base
    {
        public string Name { get; set; }
        public decimal XLocation { get; set; }
        public decimal YLocation { get; set; }
    }
}
