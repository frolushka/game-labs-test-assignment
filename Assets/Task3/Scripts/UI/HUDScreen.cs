using Task3.Core;
using UnityEngine;

namespace Task3.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class HUDScreen : MonoBehaviour
	{
		private GameManager _gm;
		private CanvasGroup _cg;

		private void Awake()
		{
			_gm = GameManager.Instance;
			_cg = GetComponent<CanvasGroup>();
		}

		private void OnEnable()
		{
			_gm.GameStarted.AddListener(HandleGameStart);
		}

		private void OnDisable()
		{
			_gm.GameStarted.RemoveListener(HandleGameStart);
		}

		private void HandleGameStart()
		{
			_cg.interactable = false;
		}
	}
}