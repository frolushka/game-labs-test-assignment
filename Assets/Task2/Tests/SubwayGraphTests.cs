using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Task2.Tests
{
	public class SubwayGraphTests
	{
		private static string[] _codes = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O" };
		[Test]
		public void NoExceptions([ValueSource("_codes")] string code1, [ValueSource("_codes")] string code2)
		{
			Assert.DoesNotThrow(() =>
			{
				var subway = CreateDefaultSubway();

				var shortestPath = subway.CalculateShortestPath(code1, code2);
			});
		}

		[Test]
		public void FromAToA()
		{
			var subway = CreateDefaultSubway();
			var code1 = "A";
			var code2 = "A";

			var shortestPath = subway.CalculateShortestPath(code1, code2);

			Assert.AreEqual(1, shortestPath.Path.Length);
			Assert.AreEqual(0, shortestPath.Transfers);
		}

		[Test]
		public void FromAToD()
		{
			var subway = CreateDefaultSubway();
			var code1 = "A";
			var code2 = "D";

			var shortestPath = subway.CalculateShortestPath(code1, code2);

			Assert.AreEqual(4, shortestPath.Path.Length);
			Assert.AreEqual(0, shortestPath.Transfers);
		}

		[Test]
		public void FromDToA()
		{
			var subway = CreateDefaultSubway();
			var code1 = "D";
			var code2 = "A";

			var shortestPath = subway.CalculateShortestPath(code1, code2);

			Assert.AreEqual(4, shortestPath.Path.Length);
			Assert.AreEqual(0, shortestPath.Transfers);
		}

		[Test]
		public void FromAToK()
		{
			var subway = CreateDefaultSubway();
			var code1 = "A";
			var code2 = "K";

			var shortestPath = subway.CalculateShortestPath(code1, code2);

			Assert.AreEqual(4, shortestPath.Path.Length);
			Assert.AreEqual(1, shortestPath.Transfers);
		}

		[Test]
		public void FromEToG()
		{
			var subway = CreateDefaultSubway();
			var code1 = "E";
			var code2 = "G";

			var shortestPath = subway.CalculateShortestPath(code1, code2);

			Assert.AreEqual(3, shortestPath.Path.Length);
			Assert.AreEqual(1, shortestPath.Transfers);
		}

		[Test]
		public void FromAToG()
		{
			var subway = CreateDefaultSubway();
			var code1 = "A";
			var code2 = "G";

			var shortestPath = subway.CalculateShortestPath(code1, code2);

			Assert.AreEqual(6, shortestPath.Path.Length);
			Assert.AreEqual(2, shortestPath.Transfers);
		}

		private ISubway CreateDefaultSubway()
		{
			var subway = new SubwayGraph();
			subway.AddRoute(new[] { "A", "B", "C", "D", "E", "F" });
			subway.AddRoute(new[] { "B", "H", "J", "F", "G" });
			subway.AddRoute(new[] { "N", "L", "D", "J", "O" });
			subway.AddRoute(new[] { "C", "J", "E", "M", "L", "K", "C" });
			return subway;
		}
	}
}