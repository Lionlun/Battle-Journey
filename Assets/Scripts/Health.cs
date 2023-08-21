using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
	[SerializeField] float takeDamageCooldownRefresh = 0.5f;
	float takeDamageCooldown = 0;
    IHealthUI objectHealthtUI;
	

	void Start()
    {
        currentHealth = maxHealth;
		objectHealthtUI = GetComponent<IHealthUI>();
	}

	void Update()
	{
		if (currentHealth <= 0)
		{
            Die();
		}

        RefreshTakeDamageCooldown();
	}
	public int ReturnHealth()
	{
		return currentHealth;
	}

	public void TakeDamage(int damage)
    {
        if (takeDamageCooldown <= 0)
        {
			currentHealth -= damage;
            takeDamageCooldown = takeDamageCooldownRefresh;
            objectHealthtUI.ShowTakeDamage();
		}

		if (currentHealth < 0)
		{
			currentHealth = 0;
		}
	}

    public void GetHealth(int health)
    {
        currentHealth += health;
        if(currentHealth > 100)
        {
            currentHealth = 100;
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
