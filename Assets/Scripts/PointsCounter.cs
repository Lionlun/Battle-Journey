using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI pointsText;

	int points;

	private void OnEnable()
	{
		GlobalEvents.OnEnemyDestroyed += GetPoints;
	}
	private void OnDisable()
	{
		GlobalEvents.OnEnemyDestroyed -= GetPoints;
	}

	void GetPoints()
	{
		points += 10;
		pointsText.text = "POINTS: " + points.ToString();
	}
}
