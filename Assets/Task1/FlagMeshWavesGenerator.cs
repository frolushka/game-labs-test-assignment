using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Task1
{
	[RequireComponent(typeof(MeshFilter))]
	public class FlagMeshWavesGenerator : MonoBehaviour
	{
		private enum Mode
		{
			CPU,
			GPU,
		}

		[SerializeField] private Mode mode;
		[SerializeField] private float waveAmplitude = 1;
		[SerializeField] private float waveSpeed = 1;

		private MeshFilter _mf;

		private void Awake()
		{
			_mf = GetComponent<MeshFilter>();

			var meshRenderer = GetComponent<MeshRenderer>();
			switch (mode)
			{
				case Mode.CPU:
					meshRenderer.material.shader = Shader.Find("Custom/FlagCPU");
					var cpuWaves = gameObject.AddComponent<FlagCPUMeshWavesGenerator>();
					cpuWaves.Initialize(waveAmplitude, waveSpeed);
					break;
				case Mode.GPU:
					meshRenderer.material.shader = Shader.Find("Custom/FlagGPU");
					meshRenderer.material.SetFloat("_Amplitude", waveAmplitude);
					meshRenderer.material.SetFloat("_Speed", waveSpeed);
					break;
			}
		}

		private void Update()
		{
			if (mode != Mode.CPU)
				return;

			var meshVertices = _mf.mesh.vertices;
			var vertices = new NativeArray<Vector3>(_mf.mesh.vertices, Allocator.TempJob);
			var job = new FlagWavesGeneratorJob()
			{
				time = Time.time,
				offset = Mathf.Sin(Time.time * waveSpeed + transform.position.z) * waveAmplitude,
				speed = waveSpeed,
				amplitude = waveAmplitude,
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
			public float speed;
			public float amplitude;
			public NativeArray<Vector3> vertices;

			public void Execute(int i)
			{
				var pos = vertices[i];
				vertices[i] = new Vector3(pos.x, pos.y, -(Mathf.Sin(speed * time + pos.x) * amplitude - offset));
			}
		}
	}
}