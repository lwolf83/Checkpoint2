using System;

namespace WCS
{
    public class Event
    {
        public int IdEvent { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }

        public Event()
        {
        }

        public Event(String description)
        {
            Description = description;
        }

        public void Postpone(TimeSpan timeDelta)
        {
            StartTime = StartTime + timeDelta;
            EndTime = EndTime + timeDelta;
        }
    }
}