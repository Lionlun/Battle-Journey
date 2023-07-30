using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField] private List<GameObject> points = new List<GameObject>();
    [SerializeField] private GameObject player;
	[SerializeField] SwipeDetection swipeDetection;
	[SerializeField] TouchDetection touchDetection;

	private void OnEnable()
	{
		InputEvents.OnTouch += Aim;
		InputEvents.OnEndTouch += Disable;
		//TouchDetection.OnTouch += Aim;
		//TouchDetection.OnEndTouch += Disable;
	}
	private void OnDisable()
	{
		InputEvents.OnTouch -= Aim;
		InputEvents.OnEndTouch -= Disable;
		//TouchDetection.OnTouch -= Aim;
		//TouchDetection.OnEndTouch -= Disable;
	}

	private void FixedUpdate()
	{
		Rotate();
		Stretch();
	}

	void Rotate()
	{
		if (touchDetection.CurrentDirection != Vector3.zero)
		{
			var relative = (transform.position + touchDetection.CurrentDirection.ToIso()) - transform.position;
			var rot = Quaternion.LookRotation(relative, Vector2.up);
			transform.rotation = rot;
		}
	}

	void Stretch()
	{
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, touchDetection.Distance/120);
	}

	public void Aim()
    {
		transform.position = player.transform.position + new Vector3(0, 2, 0);

        for (int i = 0; i < points.Count; i++)
        {
            points[i].SetActive(true);
        }
	}

    public void Enable()
    {
		this.gameObject.SetActive(true);
	}
    public void Disable()
    {
		for (int i = 0; i < points.Count; i++)
		{
			points[i].SetActive(false);
		}
	}
}
