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


        public List<Event> GetEvents(DateTime startDate, DateTime EndDate)
        {
            return Events.Where(x => IsEventInRange(x, startDate, EndDate)).ToList();
        }

        private static bool IsEventInRange(Event x, DateTime StartDate, DateTime EndDate)
        {
            Boolean eventBeforeDates = ((DateTime.Compare(x.StartTime, StartDate) < 0 && DateTime.Compare(x.EndTime, StartDate) < 0));
            Boolean eventAfterDates = ((DateTime.Compare(x.StartTime, EndDate) > 0 && DateTime.Compare(x.EndTime, EndDate) > 0));
            if (eventBeforeDates || eventAfterDates)
            {
                return false;
            }
            return true;
        }
    }
}
