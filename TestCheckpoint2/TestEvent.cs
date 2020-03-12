using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WCS;

namespace WCSTest
{
	[TestFixture]
	public class TestEvent
	{
		private Agenda _agenda;

		[SetUp]
		public void SetUp()
		{
			_agenda = new Agenda();
			_agenda.Events = new List<Event>
			{
				new Event { StartTime = new DateTime(2020, 03, 04),
							EndTime = new DateTime(2020, 06, 02) },
				new Event { StartTime = new DateTime(2020, 05, 01),
							EndTime = new DateTime(2020, 06, 15) },
				new Event { StartTime = new DateTime(2020, 05, 01),
							EndTime = new DateTime(2020, 05, 14) },
				new Event { StartTime = new DateTime(2020, 07, 12),
							EndTime = new DateTime(2020, 08, 11) }
			};					
		}


		[Test]
		public void TestEventPostponed()
		{
			Event newEvent = new Event("TestPresent");
			newEvent.StartTime = DateTime.Now - TimeSpan.FromMinutes(1);
			newEvent.EndTime = newEvent.StartTime + TimeSpan.FromHours(1);
			DateTime startDateBeforePostpone = newEvent.StartTime;
			DateTime endDateBeforePostpone = newEvent.EndTime;

			newEvent.Postpone(TimeSpan.FromDays(1));

			Assert.AreEqual(startDateBeforePostpone, newEvent.StartTime - TimeSpan.FromDays(1));
			Assert.AreEqual(endDateBeforePostpone, newEvent.EndTime - TimeSpan.FromDays(1));
		}

		[Test]
		public void TestGetEventList()
		{
			DateTime StartDate = new DateTime(2020, 05, 01);
			DateTime EndDate = new DateTime(2020, 06, 01);

			List<Event> resultTestSet = _agenda.GetEvents(StartDate, EndDate);

			Assert.AreEqual(resultTestSet, _agenda.Events.Take(3));
		}

		
		[Test]
		public void TestGetEmptyList()
		{
			DateTime StartDate2 = new DateTime(2020, 05, 01);
			DateTime EndDate2 = new DateTime(2020, 03, 01);

			List<Event> resultTestSet2 = _agenda.GetEvents(StartDate2, EndDate2);
			Assert.AreEqual(resultTestSet2.Count, 0);
		}
	}
}
