using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Task1
{
	public class FlagCPUMeshWavesGenerator : MonoBehaviour
	{
		private struct FlagWavesGeneratorJob : IJobParallelFor
		{
			public float time;
			public float speed;
			public float amplitude;

			public Vector3 flagRoot;
			public Vector2 flagSize;

			public NativeArray<Vector3> vertices;

			public void Execute(int i)
			{
				var pos = vertices[i];
				var distModifier = (pos.x - flagRoot.x) / flagSize.x;
				vertices[i] = new Vector3(pos.x, pos.y, Mathf.Sin(pos.x - speed * time) * amplitude * distModifier);
			}
		}

		private float _waveAmplitude;
		private float _waveSpeed;

		private float _scrollSpeedX;

		private FlagMeshGenerator _fmg;
		private MeshRenderer _mr;
		private MeshFilter _mf;

		private void Awake()
		{
			_fmg = GetComponent<FlagMeshGenerator>();
			_mr = GetComponent<MeshRenderer>();
			_mf = GetComponent<MeshFilter>();
		}

		private void Update()
		{
			GenerateFlagWave();
			GenerateFlagScroll();
		}

		private void GenerateFlagWave()
		{
			var meshVertices = _mf.mesh.vertices;
			var vertices = new NativeArray<Vector3>(_mf.mesh.vertices, Allocator.TempJob);
			var job = new FlagWavesGeneratorJob()
			{
				time = Time.time,
				speed = _waveSpeed,
				amplitude = _waveAmplitude,
				flagRoot = transform.position,
				flagSize = _fmg.Size,
				vertices = vertices,
			};
			var jobHandle = job.Schedule(vertices.Length, 64);
			jobHandle.Complete();

			// Weak place, but NativeArray.Copy work incorrectly.
			_mf.mesh.vertices = vertices.ToArray();
			_mf.mesh.RecalculateNormals();
			_mf.mesh.RecalculateTangents();
			_mf.mesh.RecalculateBounds();
			vertices.Dispose();
		}

		private void GenerateFlagScroll()
		{
			_mr.material.mainTextureOffset = new Vector2(Time.time * _scrollSpeedX, 0);
		}

		public void Initialize(float waveAmplitude, float waveSpeed, float scrollSpeedX)
		{
			_waveAmplitude = waveAmplitude;
			_waveSpeed = waveSpeed;
			_scrollSpeedX = scrollSpeedX;
		}
	}
}