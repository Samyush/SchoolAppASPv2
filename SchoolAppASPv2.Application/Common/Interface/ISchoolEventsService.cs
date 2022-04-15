﻿using SchoolAppASPv2.Core.Entities;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Application.Common.Interface
{
    public interface ISchoolEventsService
    {
        Task<dynamic> AddEvents(Events eventData);
        //IEnumerable<dynamic> GetEvents();
        dynamic GetEvents();
        dynamic GetSpecificEvents(int id);
        Task<dynamic> UpdateEvents(Events eventChanges);
        Task<Events> DeleteEventsAsync(int id);

    }
}
