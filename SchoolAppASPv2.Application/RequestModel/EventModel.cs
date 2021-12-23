using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Application.RequestModel
{
    public class EventModel
    {

        [Required]
        public string EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        //Accademic Events are catogerized as 1 and Extra Act are 0
        public int EventType { get; set; }

        public string Venue { get; set; }

    }
}
