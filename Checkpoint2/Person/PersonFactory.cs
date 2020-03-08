using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public class PersonFactory
    {
        public static AbstractPerson Create(string typeOfPerson)
        {
            AbstractPerson personResult;
            if (typeOfPerson == typeof(LeadFormer).Name)
            {
                personResult = new LeadFormer();
            }
            else if (typeOfPerson == typeof(Former).Name)
            {
                personResult = new Former();
            }
            else
            {
                personResult = new Student();
            }

            return personResult;
        }

    }

   
}
