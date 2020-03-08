using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public class Campus
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public List<Cursus> Cursus { get; set; }
        public List<AbstractPerson> Peoples { get; set; }
    }
}
