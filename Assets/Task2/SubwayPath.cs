namespace Task2
{
	public readonly struct SubwayPath
	{
		public readonly string[] path;
		public readonly int transfers;

		public SubwayPath(string[] path, int transfers)
		{
			this.path = path;
			this.transfers = transfers;
		}
	}
}