using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectureTest : MonoBehaviour
{

	public static InteractorsBase interactorsBase;
	public static RepositoriesBase repositoriesBase;


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

		this.StartCoroutine(this.StartGameRoutine());
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

	private IEnumerator StartGameRoutine()
	{
		interactorsBase = new InteractorsBase();
		repositoriesBase = new RepositoriesBase();

		interactorsBase.CreateAllInteractors();
		repositoriesBase.CreateAllRepositories();
		yield return null;
		interactorsBase.SendOnCreateToAllInteractors();
		repositoriesBase.SendOnCreateToAllRepositories(); 
		yield return null;
		interactorsBase.InitializeAllInteractors();
		repositoriesBase.InitializeAllRepositories();
		yield return null;
		interactorsBase.SendOnStartToAllInteractors();
		repositoriesBase.SendOnStartToAllRepositories();
		yield return null;

	}
}
