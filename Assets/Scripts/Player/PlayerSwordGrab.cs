
using UnityEngine;

public class PlayerSwordGrab : MonoBehaviour
{
	public bool IsInHands { get; set; }
	[SerializeField] private Sword sword;
	[SerializeField] private Hinge hinge;
	[SerializeField] TouchDetection touchDetection;
	private float holdTimeToUnstuck;
	private float holdTimeRefresh = 0.5f;
	private float minDistanceToGrab = 1.5f;

	private void OnEnable()
	{
		InputEvents.OnTouch += GrabSword;
		InputEvents.OnEndTouch += ReleaseSword;
	}
	private void OnDisable()
	{
		InputEvents.OnTouch -= GrabSword;
		InputEvents.OnEndTouch -= ReleaseSword;
	}

	private void Start()
	{
		holdTimeToUnstuck = holdTimeRefresh;
	}

	private void Update()
	{
		HandleSwordUnstuck();
		ControlSwordHinge();
		HandleTimer();
	}

	private void ControlSwordHinge()
	{
		if (sword.IsStuck && IsInHands && holdTimeToUnstuck < 0 && CheckDistanceToSword() < minDistanceToGrab)
		{
			hinge.Activate();
		}
		else
		{
			hinge.Deactivate();
		}
	}

	public void ReleaseSword()
	{
		IsInHands = false;
		holdTimeToUnstuck = holdTimeRefresh;
	}

	private void GrabSword()
	{
        if (CheckDistanceToSword() < minDistanceToGrab && sword.IsStuck)
        {
			IsInHands = true;
		}
	}

	private float CheckDistanceToSword()
	{
		var distanceToSword = (sword.transform.position - transform.position).magnitude;
		return distanceToSword;
	}

	private void HandleTimer()
	{
		if (IsInHands)
		{
			holdTimeToUnstuck -= Time.deltaTime;
		}
	}
	private void HandleSwordUnstuck()
	{
		if (holdTimeToUnstuck < 0)
		{
			if (touchDetection.Distance > 100 && IsInHands && CheckDistanceToSword() < minDistanceToGrab)
			{
				sword.Unstuck();
			}
		}
		
		
	}
}
