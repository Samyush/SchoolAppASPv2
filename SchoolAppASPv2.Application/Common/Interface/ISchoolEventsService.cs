using Microsoft.AspNetCore.Mvc;
using SchoolAppASPv2.Application.RequestModel;
using SchoolAppASPv2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Application.Common.Interface
{
    public interface ISchoolEventsService<T>
    {
        Task<dynamic> AddEvents(Events eventData);
        //IEnumerable<dynamic> GetEvents();
        dynamic GetEvents();

        Task<T> GetEventSpecific(int id);
        Task<dynamic> UpdateEvents(Events eventChanges);
        Task<Events> DeleteEventsAsync(int id);

    }
}
