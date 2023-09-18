using UnityEngine;

public class PlayerAimRange : MonoBehaviour
{
	[SerializeField] private LayerMask targetMask;
	[SerializeField] private LayerMask obstructionMask;
	[SerializeField] [Range(0,360)] private float angle = 360;
	public float Radius = 200;
	public Vector3 DirectionToTarget { get; set; }

	public bool LookForTarget()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius, targetMask);

		foreach(var collider in hitColliders)
		{
			if (collider.gameObject.GetComponent<EnemyBase>() != null)
			{
				DirectionToTarget = (collider.transform.position - transform.position).normalized;

				if (Vector3.Angle(transform.forward, DirectionToTarget) < angle / 2)
				{
					float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);

					if(Physics.Raycast(transform.position, DirectionToTarget, distanceToTarget, obstructionMask)) 
					{
						return false;
					}
					else
					{
						return true;
					}
				}
				else
				{
					return false;
				}
			}
		}

		return false;
	}
}
