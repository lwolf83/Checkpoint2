using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public abstract class AbstractPerson
    {
        public int IdPerson { get; set; }
        public string Name { get; set; }
        public List<AbstractPerson> AbstractPeople { get; set; }
        public int IdAgenda { get; set; }
        public Agenda Agenda { get; set; }
    }
}
