using AdessoRideShare.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Api.Helper
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AdessoRideShareContext(
                serviceProvider.GetRequiredService<DbContextOptions<AdessoRideShareContext>>()))
            {

               

                #region Create Cities

                int MaxWidth = 1000, MaxLength = 500, Parcel = 50;
                int counter = 1;
                for (int i = 0; i < MaxWidth / Parcel; i++)
                {
                    for (int j = 0; j < MaxLength / Parcel; j++)
                    {
                        context.Cities.Add(new Db.Entity.City
                        {
                            Name = $"{counter}. Şehir",
                            XLocation = i * Parcel,
                            YLocation = j * Parcel,
                            RecordId = counter
                        });
                        counter++;
                    }
                }

                #endregion

                #region Default User
                //context.Users.Add(new Db.Entity.User
                //{
                //    RecordId = 1,
                //    Code = "test",
                //    UserName = "test",
                //    Password = "test1",
                //    Name = "Samet",
                //    Surname = "Erbek"
                //});
                #endregion
                context.SaveChanges();
            }
        }
    }
}
