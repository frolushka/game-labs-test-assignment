using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Task2.Tests
{
	public class SubwayGraphTests
	{
		[Test]
		public void NoExceptionsOnCreation()
		{
			Assert.DoesNotThrow(() =>
			{
				CreateDefaultSubway();
			});
		}

		[Test]
		public void FromAToA()
		{
			var subway = CreateDefaultSubway();
			var code1 = "A";
			var code2 = "A";

			var shortestPath = subway.CalculateShortestPath(code1, code2);

			Assert.AreEqual(shortestPath.Path.Length, 1);
			Assert.AreEqual(shortestPath.Transfers, 0);
		}

		[Test]
		public void FromAToD()
		{
			var subway = CreateDefaultSubway();
			var code1 = "A";
			var code2 = "D";

			var shortestPath = subway.CalculateShortestPath(code1, code2);

			Assert.AreEqual(shortestPath.Path.Length, 4);
			Assert.AreEqual(shortestPath.Transfers, 0);
		}

		[Test]
		public void FromAToK()
		{
			var subway = CreateDefaultSubway();
			var code1 = "A";
			var code2 = "K";

			var shortestPath = subway.CalculateShortestPath(code1, code2);

			Assert.AreEqual(shortestPath.Path.Length, 4);
			Assert.AreEqual(shortestPath.Transfers, 1);
		}

		private ISubway CreateDefaultSubway()
		{
			var subway = new SubwayGraph();
			subway.AddRoute(new[] { "A", "B", "C", "D", "E", "F" });
			subway.AddRoute(new[] { "B", "H", "J", "F", "G" });
			subway.AddRoute(new[] { "N", "L", "D", "J", "O" });
			subway.AddRoute(new[] { "C", "J", "E", "M", "L", "K" });
			return subway;
		}
	}
}