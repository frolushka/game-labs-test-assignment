using System;
using UnityEngine;
using UnityEngine.UI;

namespace Task3
{
	[RequireComponent(typeof(Button))]
	public class StartButton : MonoBehaviour
	{
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(HandleClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(HandleClick);
		}

		private void HandleClick()
		{
			GameManager.Instance.RequestGameStart();
		}
	}
}