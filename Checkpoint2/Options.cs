
using CommandLine;
using System;

namespace WCS
{

    class Options
    {
    }


    [Verb("cursus", HelpText = "Get informations about the wild cursus.")]
    class CursusOptions : Options
    {
        [Option('n', "name", Required = true, HelpText = "Select a student")]
        public string name { get; set; }

        [Option('s', "students", Required = false, HelpText = "Display all the students")]
        public string Name { get; set; }

        [Option('q', "quests", Required = false, HelpText = "Display all quests")]
        public string Location { get; set; }
    }

    [Verb("events", HelpText = "Get informations about events.")]
    class EventsOptions : Options
    {
        [Option('p', "person", Required = true, HelpText = "Select a person", MetaValue = "person name")]
        public string Person { get; set; }

        [Option('b', "begins", Required = true, HelpText = "Select a person", MetaValue = "YYYY-MM-DD")]
        public DateTime Begins { get; set; }

        [Option('e', "ends", Required = true, HelpText = "Select a person", MetaValue = "YYYY-MM-DD")]
        public DateTime Ends { get; set; }
    }

}
