using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectureTest : MonoBehaviour
{
	public static SceneManagerBase sceneManager;

	private void OnEnable()
	{
		InputEvents.OnTap += SpendCoins;
	}

	private void OnDisable()
	{
		InputEvents.OnTap -= SpendCoins;
	}
	private void Start()
	{
		sceneManager = new SceneManagerExample();
		sceneManager.InitScenesMap();
		sceneManager.LoadCurrentSceneAsync();
	}

	void AddCoins()
	{
		Bank.AddCoins(this, 5);
		Debug.Log($"COINS: {Bank.Coins}");
	}

	void SpendCoins()
	{
		Bank.Spend(this, 10);
		Debug.Log($"COINS: {Bank.Coins}");
	}
}
