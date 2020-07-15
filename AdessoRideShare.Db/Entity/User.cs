using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Db.Entity
{
    public class User : Base
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
