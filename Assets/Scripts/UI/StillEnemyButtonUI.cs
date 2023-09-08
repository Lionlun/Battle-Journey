using TMPro;
using UnityEngine;

public class StillEnemyButtonUI : MonoBehaviour
{
	[SerializeField] StillEnemyButton timer;
    TextMeshProUGUI text;

	private void Start()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		text.text = timer.Timer.ToString();
	}
}
