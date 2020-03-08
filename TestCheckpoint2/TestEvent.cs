using NUnit.Framework;
using System;
using System.Collections.Generic;
using WCS;

namespace WCSTest
{
	[TestFixture]
	public class TestEvent
	{
		[SetUp]
		public void SetUp()
		{
			
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
			List<Event> eventTestSet = new List<Event>();

			Event event1 = new Event();
			event1.StartTime = new DateTime(2020, 03, 04);
			event1.EndTime = new DateTime(2020, 06, 02);
			Event event2 = new Event();
			event2.StartTime = new DateTime(2020, 05, 01);
			event2.EndTime = new DateTime(2020, 06, 15);
			Event event3 = new Event();
			event3.StartTime = new DateTime(2020, 05, 01);
			event3.EndTime = new DateTime(2020, 05, 14);
			Event event4 = new Event();
			event4.StartTime = new DateTime(2020, 07, 12);
			event4.EndTime = new DateTime(2020, 08, 11);

			eventTestSet.Add(event1);
			eventTestSet.Add(event2);
			eventTestSet.Add(event3);
			eventTestSet.Add(event4);
			Agenda TestAgenda = new Agenda();
			TestAgenda.Events = eventTestSet;

			DateTime StartDate = new DateTime(2020, 05, 01);
			DateTime EndDate = new DateTime(2020, 06, 01);
			List<Event> resultTestSet = TestAgenda.GetEvents(StartDate, EndDate);

			DateTime StartDate2 = new DateTime(2020, 05, 01);
			DateTime EndDate2 = new DateTime(2020, 03, 01);
			List<Event> resultTestSet2 = TestAgenda.GetEvents(StartDate2, EndDate2);

			Assert.AreEqual(3,resultTestSet.Count);
			Assert.AreEqual(0, resultTestSet2.Count);
			Assert.AreEqual(event1.StartTime, resultTestSet[0].StartTime);
			Assert.AreEqual(event3, resultTestSet[2]);
		}
	}
}
