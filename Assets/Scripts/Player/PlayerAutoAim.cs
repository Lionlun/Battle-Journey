using UnityEngine;

[RequireComponent(typeof(PlayerAimRange))] 
public class PlayerAutoAim : MonoBehaviour
{
    private PlayerAimRange playerAimRange;

	void Start()
	{
		playerAimRange = GetComponent<PlayerAimRange>();
	}

	public void AimToTarget()
	{
		if (playerAimRange.LookForTarget())
		{
			var directionToTarget = playerAimRange.DirectionToTarget;
			var noYRelative = new Vector3(directionToTarget.x, 0, directionToTarget.z);
			var rot = Quaternion.LookRotation(noYRelative, Vector2.up);
			transform.rotation = rot;
		}
	}
}
