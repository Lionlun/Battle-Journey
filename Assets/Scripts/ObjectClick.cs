using UnityEngine;

public class ObjectClick : MonoBehaviour
{
	[SerializeField] private RaycastHit raycastHit;
	[SerializeField] private LayerMask layerToIgnore;

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

		if (Physics.Raycast(ray, out raycastHit, float.MaxValue, ~layerToIgnore))
		{
			if (raycastHit.transform.gameObject.GetComponent<PlayerController>() != null)
			{
				var selectedPlayer = raycastHit.transform.gameObject.GetComponent<PlayerSwordGrab>();
				selectedPlayer.GrabSword();
			}
		}
	}
}
