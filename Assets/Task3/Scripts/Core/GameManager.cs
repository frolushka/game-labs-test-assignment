using UnityEngine;
using UnityEngine.Events;

namespace Task3.Core
{
	[DefaultExecutionOrder(-100)]
	public class GameManager : MonoBehaviour
	{
		public class GameStartedEvent : UnityEvent {}
		public class GameFinishedEvent : UnityEvent<Ship> {}

		public static GameManager Instance { get; private set; }

		public GameStartedEvent GameStarted { get; } = new();
		public GameFinishedEvent GameFinished { get; } = new();

		private void Awake()
		{
			Instance = this;
		}

		public void RequestGameStart()
		{
			GameStarted.Invoke();
		}

		public void RequestGameFinish(Ship winner)
		{
			GameFinished.Invoke(winner);
		}
	}
}