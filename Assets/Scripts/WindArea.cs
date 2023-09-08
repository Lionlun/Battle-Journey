using System;
using UnityEngine;

public class WindArea : MonoBehaviour
{	
	[SerializeField] private float strength;
	[SerializeField] private LayerMask layerMask;
	private float raycastRange = 15f;

	private void FixedUpdate()
	{
		RotateWindTowardsDistantWall();
	}
	public Vector3 GetDirectionForce()
	{
		return transform.right * strength;
	}

	private void RotateWindTowardsDistantWall()
	{
		if (GetDistanceToWall() < 2)
		{
			transform.RotateAround(transform.position, transform.up, 180f);
		}
	}

	private float GetDistanceToWall() // ToDo сделать отдельный класс, который рассчитывает дистанцию до объектов и добавить его к Laser и Wind Area
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.right, out hit, raycastRange, layerMask, QueryTriggerInteraction.Ignore))
		{
			if (hit.collider.gameObject.GetComponent<Wall>() != null)
			{
				Debug.Log("Wind " + hit.distance);
				Debug.DrawLine(transform.position, transform.right, Color.red);
				return hit.distance;
			}
		}

		return Int32.MaxValue;
	}
}
