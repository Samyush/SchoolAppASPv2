using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Application.Common.Interface
{
    public interface IDateTimeService
    {
        bool IsValidDate(string dateTime);
        DateTime Now { get; }
    }
}
