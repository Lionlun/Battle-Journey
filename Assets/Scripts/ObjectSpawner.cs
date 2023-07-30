using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	[SerializeField] private GameObject target;
	[SerializeField] private GameObject trap;

	float timeToSpawnTrap = 3f;
	float timeToSpawnTarget = 4f;

	Vector3 trapSpawnPosition;
	Vector3 targetSpawnPosition;

	private void Start()
	{
		//StartCoroutine(SpawnTrap());
		//StartCoroutine(SpawnTarget());
	}
	IEnumerator SpawnTrap()
	{
		trapSpawnPosition = new Vector3(Random.Range(-4, 5), -1, 1);
		yield return new WaitForSeconds(timeToSpawnTrap);
		Instantiate(trap, trapSpawnPosition, Quaternion.identity);
		StartCoroutine(SpawnTrap());
	}
	IEnumerator SpawnTarget()
	{
		targetSpawnPosition = new Vector3(Random.Range(-4, 5), -1, 1);
		yield return new WaitForSeconds(timeToSpawnTarget);
		Instantiate(target, targetSpawnPosition, Quaternion.identity);
		StartCoroutine(SpawnTarget());
	}
}
