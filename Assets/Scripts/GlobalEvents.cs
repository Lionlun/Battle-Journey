using System;

public class GlobalEvents
{
	public static event Action OnEnemyDestroyed;
	public static event Action OnPlayerDestroyed;

	public static void SendEnemyDestroyed()
	{
		OnEnemyDestroyed?.Invoke();
	}

	public static void SendPlayerDestroyed()
	{
		OnPlayerDestroyed?.Invoke();
	}
}
