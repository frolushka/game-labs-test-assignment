namespace Task2
{
	public interface ISubway
	{
		void AddRoute(string[] stationCodes);
		SubwayPath CalculateShortestPath(string stationCode1, string stationCode2);
	}
}