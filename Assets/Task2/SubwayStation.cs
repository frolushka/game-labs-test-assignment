using System.Collections.Generic;

namespace Task2
{
	public class SubwayStation
	{
		public string StationCode { get; }
		public List<SubwayStation> Connections { get; } = new();
		public List<SubwayStation> Intersectrions { get; } = new();

		public SubwayStation(string stationCode)
		{
			StationCode = stationCode;
		}
	}
}