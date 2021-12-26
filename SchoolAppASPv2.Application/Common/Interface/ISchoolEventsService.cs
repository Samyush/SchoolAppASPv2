using SchoolAppASPv2.Application.RequestModel;
using SchoolAppASPv2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Application.Common.Interface
{
    public interface ISchoolEventsService
    {
        Task<dynamic> AddEvents(Events eventData);
        //IEnumerable<dynamic> GetEvents();
        dynamic GetEvents();
        Events UpdateEvents(int id);
        Task<Events> DeleteEventsAsync(int id);

    }
}
