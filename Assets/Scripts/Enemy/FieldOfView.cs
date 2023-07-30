using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView: MonoBehaviour
{
	[Range(0, 360)] public float Angle;
	public bool IsPlayerSeen;
	public float Radius;
	public Vector3 DirectionToTarget;
	
	public PlayerController Player;

	[SerializeField] LayerMask targetMask;
	[SerializeField] LayerMask obstructionMask;

	WaitForSeconds wait;

	void Start()
    {
		wait = new WaitForSeconds(0.2f);
		StartCoroutine(ManageFieldOfView());
	}
	
	private IEnumerator ManageFieldOfView() 
	{
		while (true)
        {
			yield return wait;
			FieldOfViewCheck();
		}
	}

	void FieldOfViewCheck()
	{
		Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius, targetMask);

		if (rangeChecks.Length != 0 ) 
		{
			Transform target = rangeChecks[0].transform;
			DirectionToTarget = (target.position - transform.position).normalized; 

			if(Vector3.Angle(transform.forward, DirectionToTarget) < Angle / 2)
			{
				float distanceToTarget = Vector3.Distance(transform.position, target.position);

				if(!Physics.Raycast(transform.position, DirectionToTarget, distanceToTarget, obstructionMask))
				{
					IsPlayerSeen = true;
				}
				else
				{
					IsPlayerSeen = false;
				}
			}
			else
			{
				IsPlayerSeen = false;
			}
		}
		else if (IsPlayerSeen) 
		{
			IsPlayerSeen = false;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			//var player = collision.gameObject.GetComponent<PlayerController>();
			//player.Attack();
			//EventManager.SendOnTargetDestroyed();
			//Destroy(gameObject);
		}
	}
}
