using Task3.Core;
using TMPro;
using UnityEngine;

namespace Task3.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class WinScreen : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI winText;

		private GameManager _gm;
		private CanvasGroup _cg;

		private void Awake()
		{
			_gm = GameManager.Instance;
			_cg = GetComponent<CanvasGroup>();
		}

		private void OnEnable()
		{
			_gm.GameFinished.AddListener(HandleGameFinish);
		}

		private void OnDisable()
		{
			_gm.GameFinished.RemoveListener(HandleGameFinish);
		}

		private void HandleGameFinish(Ship winner)
		{
			_cg.alpha = 1;
			_cg.interactable = true;
			_cg.blocksRaycasts = true;
			winText.text = $"{winner.Config.shipName} win!";
		}
	}
}