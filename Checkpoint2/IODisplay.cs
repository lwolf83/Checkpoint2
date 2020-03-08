using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public class IODisplay
    {
        public static void Events(List<Event> events)
        {
            string array = "";
            string lignTemplate = "|{0, -20} | {1,40} | {2, 10:dd/MM/yyyy}| {3, 10:dd/MM/yyyy}|\n";
            string header = String.Format(lignTemplate, "Event", "Description", "Start", "End");
            string separationLine = new String('-', header.Length - 1) + "\n";

            array += separationLine + header + separationLine;

            foreach (Event currentEvent in events)
            {
                array += String.Format(lignTemplate, GetField(currentEvent.Name, 20), GetField(currentEvent.Description, 40), currentEvent.StartTime, currentEvent.EndTime);
            }
            array += separationLine;

            Console.WriteLine($"\n{array}");
        }

        private static string GetField(string field, int maxlength)
        {
            return field.Length > maxlength ? field.Substring(0, maxlength) : field;
        }

    }
}
