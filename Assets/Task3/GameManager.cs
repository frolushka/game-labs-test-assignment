using System;
using UnityEngine;
using UnityEngine.Events;

namespace Task3
{
	[DefaultExecutionOrder(-100)]
	public class GameManager : MonoBehaviour
	{
		public class GameStartedEvent : UnityEvent {}

		public static GameManager Instance;

		public GameStartedEvent GameStarted { get; } = new();

		private void Awake()
		{
			Instance = this;
		}

		public void RequestGameStart()
		{
			GameStarted.Invoke();
		}
	}
}