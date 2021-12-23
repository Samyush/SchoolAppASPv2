using SchoolAppASPv2.Application.Common.Interface;
using SchoolAppASPv2.Application.RequestModel;
using SchoolAppASPv2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Infastructure.Services
{
    public class SchoolEventsServices : ISchoolEventsService
    {
        public async Task<Events> AddEvents(dynamic data)
        {
            throw new NotImplementedException();
        }

        public Events GetEvents()
        {
            throw new NotImplementedException();
        }

        public Events UpdateEvents(int id)
        {
            throw new NotImplementedException();
        }
        public Events DeleteEvents(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddEvents(Events eventData)
        {
            throw new NotImplementedException();
        }
    }
}
