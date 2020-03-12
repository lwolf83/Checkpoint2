using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WCS;

namespace WCSTest
{
	[TestFixture]
	public class TestFactory
	{
		[Test]
		public void TestFactoryStudent()
		{
			AbstractPerson testPerson = PersonFactory.Create("John", new List<AbstractPerson>());
			Assert.AreEqual(typeof(Student), testPerson.GetType());
		}

		[Test]
		public void TestFactoryFormer()
		{
			AbstractPerson testPerson = PersonFactory.Create("John", new List<AbstractPerson>() { new Student() });
			Assert.AreEqual(typeof(Former), testPerson.GetType());
		}

		[Test]
		public void TestFactoryleadFormer()
		{
			AbstractPerson testPerson = PersonFactory.Create("John", new List<AbstractPerson>() { new Former() });
			Assert.AreEqual(typeof(LeadFormer), testPerson.GetType());
		}

	}
}
