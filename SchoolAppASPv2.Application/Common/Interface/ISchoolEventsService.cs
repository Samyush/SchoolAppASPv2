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
        Task<Events> AddEvents(dynamic eventData);
        Events GetEvents();
        Events UpdateEvents(int id);
        Events DeleteEvents(int id);

    }
}
