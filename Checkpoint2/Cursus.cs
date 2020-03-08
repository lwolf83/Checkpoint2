using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public class Cursus
    {
        public Agenda Agenda { get; set; }
        public Campus Campus { get; set; }
        public List<Expedition> Expeditions {get; set;}
    }
}
