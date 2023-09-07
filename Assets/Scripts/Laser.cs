using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
	[SerializeField] ProcedualMapGeneration procedualMapGeneration;
	[SerializeField] LayerMask layerMask;
	[SerializeField] private float laserRange = 50f;
	[SerializeField] private float laserDuration = 3f;


	private LineRenderer laserLine;

	private void Start()
	{
		laserLine = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		laserLine.SetPosition(0, transform.position);
		RaycastHit hit;
	
		if (Physics.Raycast(transform.position, transform.forward, out hit, laserRange, layerMask, QueryTriggerInteraction.Ignore))
		{
			if (hit.collider)
			{
				laserLine.SetPosition(1, hit.point);
			}
	
		}
		else
		{
				laserLine.SetPosition(1, transform.forward * laserRange);
		}

		if (Physics.Raycast(transform.position, transform.forward, out hit, laserRange, layerMask, QueryTriggerInteraction.Ignore))
		{
			if (hit.collider.gameObject.GetComponent<PlayerController>()!=null)
			{
				Debug.Log("Player lasered");
			}

		}
	}

	public float GetDistanceToWall()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit, layerMask))
		{
			if (hit.collider.gameObject.GetComponent<Wall>() != null)
			{
				Debug.Log(hit.distance);
				return hit.distance;
			}
		}

		return Int32.MaxValue;
	}
}
