using Task3.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Task3.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class MenuScreen : MonoBehaviour
	{
		[SerializeField] private Button startButton;

		private GameManager _gm;
		private CanvasGroup _cg;

		private void Awake()
		{
			_gm = GameManager.Instance;
			_cg = GetComponent<CanvasGroup>();
		}

		private void OnEnable()
		{
			startButton.onClick.AddListener(HandleStartButtonClick);
		}

		private void OnDisable()
		{
			startButton.onClick.RemoveListener(HandleStartButtonClick);
		}

		private void HandleStartButtonClick()
		{
			_gm.RequestGameStart();

			_cg.alpha = 0;
			_cg.interactable = false;
			_cg.blocksRaycasts = false;
		}
	}
}