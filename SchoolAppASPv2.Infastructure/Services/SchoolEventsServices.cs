using SchoolAppASPv2.Application.Common.Interface;
using SchoolAppASPv2.Application.RequestModel;
using SchoolAppASPv2.Core.Entities;
using SchoolAppASPv2.Infastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Infastructure.Services
{
    public class SchoolEventsServices : ISchoolEventsService
    {
        private readonly SchoolAppAspDbContext databaseContext;

        public SchoolEventsServices(SchoolAppAspDbContext db)
        {
            databaseContext = db;
        }
        public async Task<dynamic> AddEvents(Events data)
        {
            try
            {
                databaseContext.Events.Add(data);
                var result = await databaseContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return null;
            }
            //throw new NotImplementedException();
        }

        public Events GetEvents()
        {
            throw new NotImplementedException();
        }

        public Events UpdateEvents(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Events> DeleteEventsAsync(int id)
        {
            var toDel = databaseContext.Events.Where(x => x.Id == id).First();
            databaseContext.Events.Remove(toDel);
            var result = await databaseContext.SaveChangesAsync();
            return toDel;
        }

    }
}
