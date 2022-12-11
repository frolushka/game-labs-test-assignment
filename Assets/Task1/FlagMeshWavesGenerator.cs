using UnityEngine;

namespace Task1
{
	[RequireComponent(typeof(FlagMeshGenerator))]
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
		[SerializeField] private float scrollSpeedX = 0;

		private void Awake()
		{
			var fmg = GetComponent<FlagMeshGenerator>();
			var mr = GetComponent<MeshRenderer>();
			switch (mode)
			{
				case Mode.CPU:
					mr.material.shader = Shader.Find("Custom/FlagCPU");
					var cpuWaves = gameObject.AddComponent<FlagCPUMeshWavesGenerator>();
					cpuWaves.Initialize(waveAmplitude, waveSpeed, scrollSpeedX);
					break;
				case Mode.GPU:
					mr.material.shader = Shader.Find("Custom/FlagGPU");
					mr.material.SetFloat("_Amplitude", waveAmplitude);
					mr.material.SetFloat("_Speed", waveSpeed);
					mr.material.SetVector("_FlagRoot", transform.position);
					mr.material.SetVector("_FlagSize", fmg.Size);
					mr.material.SetFloat("_ScrollSpeedX", scrollSpeedX);
					break;
			}
		}
	}
}