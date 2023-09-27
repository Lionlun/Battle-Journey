using UnityEngine;

public class SwordWallStuck : MonoBehaviour
{
	public bool IsInWall { get; set; }
	public bool IsUnstucking { get; set; }

	[SerializeField] private PlayerSwordGrab playerGrab;
	[SerializeField] Hinge hinge;
	[SerializeField] private PlayerController player;
	[SerializeField] Transform swordDownPosition;
	[SerializeField] private PlayerAttackDash playerAttack;

	private void OnEnable()
	{
		InputEvents.OnEndTouch += FinishUnstucking;
	}

	private void OnDisable()
	{
		InputEvents.OnEndTouch -= FinishUnstucking;
	}

	private void ObjectStuck()
	{
		hinge.Deactivate();
		transform.parent = null;
		IsInWall = true;
		playerGrab.ReleaseSword();
	}

	public void Unstuck()
	{
		hinge.Deactivate();

		IsInWall = false;
		IsUnstucking = true;
		this.transform.parent = null;
		SetSwordDefaultPosition();
		this.transform.parent = player.transform;
		player.MoveBack();
	}

	private void SetSwordDefaultPosition()
	{
		IsInWall = false;
		transform.position = swordDownPosition.position;
		transform.rotation = swordDownPosition.rotation;
	}

	private void FinishUnstucking()
	{
		IsUnstucking = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Wall>() != null)
		{
			if (playerAttack.IsAttacking)
			{
				IsInWall = true;
				ObjectStuck();
				player.Stop();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.GetComponent<Wall>() != null)
		{
			IsInWall = false;
		}
	}
}
