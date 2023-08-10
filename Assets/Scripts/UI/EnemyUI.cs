using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour, IHealthUI
{
	[SerializeField] private HealthUI healthUI;
	[SerializeField] private Image image;
	[SerializeField] private Sprite fullHealth;
	[SerializeField] private Sprite midHealth;
	[SerializeField] private Sprite lowHealth;

	private Health health;

	void Start()
	{
		health = GetComponent<Health>();
	}

	public void ShowTakeDamage()
	{
		if (health.ReturnHealth() > 66)
		{
			image.sprite = fullHealth;
		}

		if (health.ReturnHealth() <= 66)
		{
			image.sprite = midHealth;
		}

		if (health.ReturnHealth() <= 33)
		{
			image.sprite = lowHealth;
		}

		healthUI.gameObject.SetActive(true);
	}
}
