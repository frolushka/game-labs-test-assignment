namespace Task2
{
	public interface ISubway
	{
		void AddRoute(string[] codes);
		SubwayPath CalculateShortestPath(string code1, string code2);
	}
}