using System;

namespace Task2
{
	public class SubwayPathNotFound : Exception
	{
		public string Code1 { get; }
		public string Code2 { get; }

		public SubwayPathNotFound(string code1, string code2)
		{
			Code1 = code1;
			Code2 = code2;
		}

		public override string ToString()
		{
			return $"Path between station \"{Code1}\" and \"{Code2}\" not found!";
		}
	}
}