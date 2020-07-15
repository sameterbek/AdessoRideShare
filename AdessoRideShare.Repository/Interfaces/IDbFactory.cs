using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Repository.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        DbContext Init();
        void SaveChanges();
    }
}
