using System;
using TMPro;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Task1
{
	[RequireComponent(typeof(MeshFilter))]
	public class FlagMeshWavesGenerator : MonoBehaviour
	{
		[SerializeField] private float amplitude = 1;

		private MeshFilter _mf;

		private void Awake()
		{
			_mf = GetComponent<MeshFilter>();
		}

		private void Update()
		{
			var meshVertices = _mf.mesh.vertices;
			var vertices = new NativeArray<Vector3>(_mf.mesh.vertices, Allocator.TempJob);
			var job = new FlagWavesGeneratorJob()
			{
				time = Time.time,
				offset = Mathf.Sin(Time.time + transform.position.z) * amplitude,
				amplitude = amplitude,
				vertices = vertices,
			};
			var jobHandle = job.Schedule(vertices.Length, 64);
			jobHandle.Complete();

			_mf.mesh.vertices = vertices.ToArray();
			_mf.mesh.RecalculateNormals();
			_mf.mesh.RecalculateTangents();
			_mf.mesh.RecalculateBounds();

			// for (var i = 0; i < vertices.Length; i++)

			// NativeArray<Vector3>.Copy(vertices, meshVertices);
			// Debug.Log($"{vertices[1]} {_mf.mesh.vertices[1]}");
			vertices.Dispose();
		}

		private struct FlagWavesGeneratorJob : IJobParallelFor
		{
			public float time;
			public float offset;
			public float amplitude;
			public NativeArray<Vector3> vertices;

			public void Execute(int i)
			{
				var pos = vertices[i];
				vertices[i] = new Vector3(pos.x, pos.y, -(Mathf.Sin(time + pos.x) * amplitude - offset));
			}
		}
	}
}