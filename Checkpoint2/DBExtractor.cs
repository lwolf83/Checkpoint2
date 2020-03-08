using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WCS
{
    class DBExtractor
    {
        public static AbstractPerson GetPersonByName(string name)
        {
            
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@Name", name)
            };
            SqlDataReader reader = Database.ExecuteStoredProcedure("sp_getPersonByName", parameters);
            AbstractPerson person;

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string typeOfPerson;
                    typeOfPerson = reader.GetString(reader.GetOrdinal("FunctionName"));
                    person = PersonFactory.Create(typeOfPerson);
                    person.Name = reader.GetString(reader.GetOrdinal("PersonName"));
                    person.IdPerson = reader.GetInt32(reader.GetOrdinal("Person_Id"));
                    person.IdAgenda = reader.GetInt32(reader.GetOrdinal("FK_Agenda_id"));
                }
                else
                {
                    throw new Exception($"No result for {name}");
                }
            }
            else
            {
                throw new Exception($"{name} match any result");
            }
            
            person.Agenda = DBExtractor.GetAgendaByIdPerson(person.IdPerson);
            
            return person;

        }

        public static Agenda GetAgendaByIdPerson(int idPerson)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@idPerson", idPerson)
            };
            SqlDataReader reader = Database.ExecuteStoredProcedure("sp_getAgendaByIdPerson", parameters);

            Agenda agenda;

            int idAgenda = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string agendaName = reader.GetString(reader.GetOrdinal("Name"));
                    idAgenda = reader.GetInt32(reader.GetOrdinal("Agenda_Id"));
                    agenda = new Agenda(idAgenda, agendaName);
                }
                else
                {
                    throw new Exception("Impossible to read the Agenda");
                }
            }
            else
            {
                agenda = new Agenda();
            }
            
            agenda.Events = DBExtractor.GetEventsByIdAgenda(idAgenda);
            return agenda;
        }

        public static List<Event> GetEventsByIdAgenda(int idAgenda)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@idAgenda", idAgenda)
            };
            SqlDataReader reader = Database.ExecuteStoredProcedure("sp_getEventsByIdAgenda", parameters);

            List <Event> Events = new List<Event>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Event currentEvent = new Event();
                    currentEvent.Name = reader.GetString(reader.GetOrdinal("Name"));
                    currentEvent.Description = reader.IsDBNull("Description") ? "" : reader.GetString(reader.GetOrdinal("Description"));
                    currentEvent.IdEvent = reader.GetInt32(reader.GetOrdinal("Event_Id"));
                    currentEvent.StartTime = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                    currentEvent.EndTime = reader.GetDateTime(reader.GetOrdinal("EndDate"));
                    Events.Add(currentEvent);
                }

            }
            return Events;
        }
    }


}
