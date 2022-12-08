using System.Collections.Generic;

namespace Task2
{
	public class SubwayStation
	{
		public string Code { get; }
		public List<SubwayStation> Connections { get; } = new List<SubwayStation>();
		public List<SubwayStation> Intersectrions { get; } = new List<SubwayStation>();

		public SubwayStation(string code)
		{
			Code = code;
		}
	}
}