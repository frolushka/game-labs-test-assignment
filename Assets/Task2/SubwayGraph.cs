using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Task2
{
	public class SubwayGraph : ISubway
	{
		private class SubwayStationStatus
		{
			public SubwayStationStatus PrevStationStatus { get; }
			public SubwayStation Station { get; }

			public SubwayStationStatus(SubwayStationStatus prevStationStatus, SubwayStation station)
			{
				PrevStationStatus = prevStationStatus;
				Station = station;
			}
		}

		private List<SubwayStation> _stations = new();

		public void AddRoute(string[] stationCodes)
		{
			SubwayStation firstStation = null;
			SubwayStation prevStation = null;
			foreach (var stationCode in stationCodes)
			{
				// Loop found.
				if (firstStation?.StationCode == stationCode)
				{
					prevStation.Connections.Add(firstStation);
					firstStation.Connections.Add(prevStation);
					break;
				}

				var newStation = new SubwayStation(stationCode);
				firstStation ??= newStation;
				foreach (var existingStation in _stations)
				{
					if (existingStation.StationCode == newStation.StationCode)
					{
						existingStation.Intersectrions.Add(newStation);
						newStation.Intersectrions.Add(existingStation);
					}
				}
				_stations.Add(newStation);

				if (prevStation != null)
				{
					newStation.Connections.Add(prevStation);
					prevStation.Connections.Add(newStation);
				}
				prevStation = newStation;
			}
		}

		public SubwayPath CalculateShortestPath(string code1, string code2)
		{
			var visited = new HashSet<SubwayStation>();

			var queue = new Queue<SubwayStationStatus>();
			var startStation = GetStation(code1);
			queue.Enqueue(new SubwayStationStatus(null, startStation));

			while (queue.TryDequeue(out var nextStatus))
			{
				visited.Add(nextStatus.Station);
				if (nextStatus.Station.StationCode == code2)
					return DecodePath(nextStatus);

				foreach (var station in nextStatus.Station.Connections)
				{
					if (!visited.Contains(station))
					{
						queue.Enqueue(new SubwayStationStatus(nextStatus, station));
					}
				}

				foreach (var intersection in nextStatus.Station.Intersectrions)
				{
					var intersectionStatus = new SubwayStationStatus(nextStatus, intersection);
					foreach (var station in intersection.Connections)
					{
						if (!visited.Contains(station))
						{
							queue.Enqueue(new SubwayStationStatus(intersectionStatus, station));
						}
					}
				}
			}

			throw new SubwayPathNotFound(code1, code2);
		}

		private SubwayPath DecodePath(SubwayStationStatus lastStatus)
		{
			string[] path = null;
			int transfers = 0;
			DecodePath(lastStatus, 1, ref path, ref transfers);
			return new SubwayPath(path, transfers);

			static void DecodePath(SubwayStationStatus lastStatus, int depth, ref string[] path, ref int transfers)
			{
				if (lastStatus.PrevStationStatus == null)
					path = new string[depth];
				else
				{
					// TODO
					var localDepth = depth;
					if (lastStatus.PrevStationStatus.Station.StationCode != lastStatus.Station.StationCode)
						localDepth++;
					else
						transfers++;
					DecodePath(lastStatus.PrevStationStatus, localDepth, ref path, ref transfers);
				}
				path[path.Length - depth] = lastStatus.Station.StationCode;
			}
		}

		private SubwayStation GetStation(string code)
		{
			return _stations.Find(x => x.StationCode == code);
		}
	}
}
