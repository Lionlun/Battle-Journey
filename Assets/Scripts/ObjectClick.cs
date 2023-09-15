using UnityEngine;

public class ObjectClick : MonoBehaviour
{
	private RaycastHit raycastHit;

	private void OnEnable()
	{
		InputEvents.OnTouch += OnClick;
	}
	private void OnDisable()
	{
		InputEvents.OnTouch -= OnClick;
	}

	public void OnClick()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out raycastHit))
		{
			if (raycastHit.transform.gameObject.GetComponent<PlayerController>() != null)
			{
				var selectedPlayer = raycastHit.transform.gameObject.GetComponent<PlayerSwordGrab>();
				selectedPlayer.GrabSword();
			}
		}
	}
}
