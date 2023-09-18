using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public int CurrentHealth { get; set; }
	[SerializeField] float takeDamageCooldownRefresh = 0.5f;
	float takeDamageCooldown = 0;
    IHealthUI objectHealthtUI;
	

	void Start()
    {
        CurrentHealth = maxHealth;
		objectHealthtUI = GetComponent<IHealthUI>();
	}

	void Update()
	{
		if (CurrentHealth <= 0)
		{
            Die();
		}

        RefreshTakeDamageCooldown();
	}
	public int ReturnHealth()
	{
		return CurrentHealth;
	}

	public void TakeDamage(int damage)
    {
        if (takeDamageCooldown <= 0)
        {
			CurrentHealth -= damage;
            takeDamageCooldown = takeDamageCooldownRefresh;
            objectHealthtUI.ShowTakeDamage();
		}

		if (CurrentHealth < 0)
		{
			CurrentHealth = 0;
		}
	}

    public void GetHealth(int health)
    {
        CurrentHealth += health;
        if(CurrentHealth > 100)
        {
            CurrentHealth = 100;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void RefreshTakeDamageCooldown()
    {
		if (takeDamageCooldown > 0)
		{
			takeDamageCooldown -= Time.deltaTime;
		}
	}
}
