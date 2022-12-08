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

		private List<SubwayStation> _stations = new List<SubwayStation>();

		public void AddRoute(string[] codes)
		{
			SubwayStation prevStation = null;
			foreach (var routeCode in codes)
			{
				var newStation = new SubwayStation(routeCode);
				foreach (var existingStation in _stations)
				{
					if (existingStation.Code == newStation.Code)
					{
						existingStation.Intersectrions.Add(newStation);
						newStation.Intersectrions.Add(newStation);
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
			var queue = new Queue<SubwayStationStatus>();
			var startStation = GetStation(code1);
			queue.Enqueue(new SubwayStationStatus(null, startStation));
			while (queue.TryDequeue(out var nextStatus))
			{
				if (nextStatus.Station.Code == code2)
					return DecodePath(nextStatus);
				foreach (var station in nextStatus.Station.Connections)
					queue.Enqueue(new SubwayStationStatus(nextStatus, station));
				foreach (var intersection in nextStatus.Station.Intersectrions)
				{
					foreach (var station in nextStatus.Station.Connections)
						queue.Enqueue(new SubwayStationStatus(nextStatus, station));

				}
			}

			throw new SubwayPathNotFound(code1, code2);
		}

		SubwayPath DecodePath(SubwayStationStatus lastStatus)
		{
			string[] path = null;
			int transfers = 0;
			DecodePath(lastStatus, 1);
			return new SubwayPath(path, transfers);

			void DecodePath(SubwayStationStatus lastStatus, int depth)
			{
				if (lastStatus.PrevStationStatus == null)
					path = new string[depth];
				else
				{
					// TODO
					var localDepth = depth;
					if (lastStatus.PrevStationStatus.Station.Code != lastStatus.Station.Code)
						localDepth++;
					else
						transfers++;
					DecodePath(lastStatus.PrevStationStatus, localDepth);
				}
				path[path.Length - depth] = lastStatus.Station.Code;
			}
		}

		private SubwayStation GetStation(string code)
		{
			return _stations.Find(x => x.Code == code);
		}
	}
}
