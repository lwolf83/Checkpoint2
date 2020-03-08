using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WCS
{
    class Program
    {
        public static void Main(string[] args)
        {

            Parser.Default.ParseArguments<CursusOptions, EventsOptions>(args)
                .WithParsed<CursusOptions>(RunCursusCommand)
                .WithParsed<EventsOptions>(RunEventCommand);

            /*
            Event newEvent = new Event("Important meeting");
            newEvent.StartTime = DateTime.Now;
            newEvent.EndTime = DateTime.Now + TimeSpan.FromHours(1);
            newEvent.Postpone(TimeSpan.FromHours(1));
            Console.WriteLine("Another meeting is postponed");*/
        }

        public static void RunCursusCommand(CursusOptions options)
        {

        }

        public static void RunEventCommand(EventsOptions options)
        {
            AbstractPerson currentPerson = DBExtractor.GetPersonByName(options.Person);
            List<Event> currentEvents = currentPerson.Agenda.GetEvents(options.Begins, options.Ends);
            IODisplay.Events(currentEvents);
        }



    }
}
