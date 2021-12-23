using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Core.Models.EventsModels.AcademicModels
{
    internal class AcademicEventsModels
    {
        public int ID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Notice { get; set; }
    }
}
