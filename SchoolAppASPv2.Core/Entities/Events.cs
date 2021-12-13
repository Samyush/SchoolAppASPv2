using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Core.Entities
{
    public class Events
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventVenue { get; set; }

    }
}
