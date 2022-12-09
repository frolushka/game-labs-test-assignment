using UnityEngine;

namespace Task1
{
	public class FlagCPUMeshWavesGenerator : MonoBehaviour
	{
		private float _waveAmplitude;
		private float _waveSpeed;

		public void Initialize(float waveAmplitude, float waveSpeed)
		{
			_waveAmplitude = waveAmplitude;
			_waveSpeed = waveSpeed;
		}
	}
}