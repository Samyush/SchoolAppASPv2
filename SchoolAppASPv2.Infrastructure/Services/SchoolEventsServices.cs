using SchoolAppASPv2.Application.Common.Interface;
using SchoolAppASPv2.Application.RequestModel;
using SchoolAppASPv2.Core.Entities;
using SchoolAppASPv2.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolAppASPv2.Infrastructure.DataBase;

namespace SchoolAppASPv2.Infrastructure.Services
{
    public class SchoolEventsServices : ISchoolEventsService
    {
        private readonly SchoolAppAspDbContext _databaseContext;

        #region SchoolEventStart
        public SchoolEventsServices(SchoolAppAspDbContext db)
        {
            _databaseContext = db;
        }
        public async Task<dynamic> AddEvents(Events data)
        {
            try
            {
                _databaseContext.Events.Add(data);
                var result = await _databaseContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return null;
            }
            //throw new NotImplementedException();
        }

        //public IEnumerable<dynamic> GetEvents()
        //the below way sends data through api with result and data two different types
        public dynamic GetEvents()
        {
            var data = _databaseContext.Events.ToArray();
            var response = new ResponseModel
            {
                result = "success",
                data = data
            };
            return response;
        }

        public dynamic GetSpecificEvents(int id)
        {
            var data = _databaseContext.Events.Where(m => m.Id == id);
            return data;
        }

        public async Task<dynamic> UpdateEvents(Events eventChanges)
        {
            //throw new NotImplementedException();
            try
            {
                _databaseContext.Attach(eventChanges);
                _databaseContext.Entry(eventChanges).Property(p => p.EventName).IsModified = true;
                var result = await _databaseContext.SaveChangesAsync();
                return result;
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public async Task<Events> DeleteEventsAsync(int id)
        {
            var toDel = _databaseContext.Events.First(x => x.Id == id);
            _databaseContext.Events.Remove(toDel);
            await _databaseContext.SaveChangesAsync();
            return toDel;
        }
        
        #endregion

    }
}
