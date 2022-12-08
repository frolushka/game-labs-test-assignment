namespace Task2
{
	public readonly struct SubwayPath
	{
		public readonly string[] Path;
		public readonly int Transfers;

		public SubwayPath(string[] path, int transfers)
		{
			Path = path;
			Transfers = transfers;
		}
	}
}