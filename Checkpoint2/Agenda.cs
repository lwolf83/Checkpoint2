using System;
using System.Collections.Generic;
using System.Linq;

namespace WCS
{
    public class Agenda
    {
        public int IdAgenda { get; set; }
        public string Name { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();

        public Agenda()
        {
        }

        public Agenda(int idAgenda, string name)
        {
            IdAgenda = idAgenda;
            Name = name;
           
        }

        public List<Event> GetEvents(DateTime startDate, DateTime endDate)
        {
            return Events.Where(x => IsEventInRange(x, startDate, endDate)).ToList();
        }

        private static bool IsEventInRange(Event x, DateTime startDate, DateTime endDate)
        {
            Boolean isInRange = false;
            if (x.StartTime <= startDate && x.EndTime >= endDate)
            {
                isInRange = true;
            }
            else if (x.EndTime <= endDate)
            {
                isInRange = true;
            }
            else if (x.StartTime >= endDate)
            {
                isInRange = false;
            }   
            return isInRange;
        }
    }
}
