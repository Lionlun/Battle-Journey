using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillEnemyButtonRelation : MonoBehaviour
{
	[SerializeField] private StillEnemyButton enemyButtonPrefab;
	private StillEnemyButton enemyButton;
	private float buttonDistance = 2f;

	private void Start()
	{
		SpawnButton();
	}
	private void Update()
	{
		
		CheckButton();
	}
	private void SpawnButton()
	{
		enemyButton = Instantiate(enemyButtonPrefab, transform.position + -transform.forward*buttonDistance, Quaternion.identity);
	}

	private void CheckButton()
	{
		if (enemyButton.IsPressed)
		{
			Destroy(enemyButton.gameObject);
			Destroy(this.gameObject);
		}
	}
}
