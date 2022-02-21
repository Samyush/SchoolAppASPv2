using Microsoft.AspNetCore.Mvc;
using SchoolAppASPv2.Application.Common.Interface;
using SchoolAppASPv2.Application.RequestModel;
using SchoolAppASPv2.Core.Entities;
using SchoolAppASPv2.Identity.Models;
using SchoolAppASPv2.Infastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Infastructure.Services
{
    public class SchoolEventsServices<T> : ISchoolEventsService<T>
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

        //public IEnumerable<dynamic> GetEvents()
        //the below way sends data through api with result and data two different types
        public dynamic GetEvents()
        {
            var data = databaseContext.Events.ToArray();
            var response = new ResponseModel
            {
                result = "success",
                data = data
            };
            return response;
        }

        public Task<T> GetEventSpecific(int id)
        {
            try
            {
                var data = databaseContext.Events.Where(x => x.Id == id).ToList();
                var response = new ResponseModel
                {
                    result = "success",
                    data = data.ToList()
                };
                return response;
            }catch (Exception)
            {
                return null;
            }
        }

        public async Task<dynamic> UpdateEvents(Events eventChanges)
        {
            //throw new NotImplementedException();
            try
            {
                databaseContext.Attach(eventChanges);
                databaseContext.Entry(eventChanges).Property(p => p.EventName).IsModified = true;
                var result = await databaseContext.SaveChangesAsync();
                return result;
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
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
