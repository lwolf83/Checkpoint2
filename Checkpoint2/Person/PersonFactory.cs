using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WCS
{
    public class PersonFactory
    {
        public static AbstractPerson Create(string name, List<AbstractPerson> subordinates = null)
        {
            if (subordinates == null)
            {
                subordinates = new List<AbstractPerson>();
            }
            AbstractPerson person = null;
            if(subordinates.Count == 0)
            {
                person = new Student { Name = name };
            }
            else if(subordinates.All(x => x.GetType() == typeof(Student)))
            {
                person = new Former { Name = name, AbstractPeople = subordinates };
            }
            else if(subordinates.Any(x => x.GetType() == typeof(Former)))
            {
                person = new LeadFormer { Name = name, AbstractPeople = subordinates };
            }
            return person;
        }

    }

   
}
