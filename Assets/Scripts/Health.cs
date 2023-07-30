using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
	[SerializeField] float takeDamageCooldownRefresh = 1.5f;
	float takeDamageCooldown = 0;
	

	void Start()
    {
        currentHealth = maxHealth;
	}

	private void Update()
	{
		if (currentHealth <= 0)
		{
            Die();
		}

        RefreshTakeDamageCooldown();
	}
	public void TakeDamage(int damage)
    {
        if (takeDamageCooldown <= 0)
        {
			currentHealth -= damage;
            takeDamageCooldown = takeDamageCooldownRefresh;
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

    private void RefreshTakeDamageCooldown()
    {
		if (currentHealth < 0)
		{
			currentHealth = 0;
		}
	}
}
