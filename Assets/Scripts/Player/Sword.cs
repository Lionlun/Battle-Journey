using UnityEngine;

public class Sword : MonoBehaviour
{
	[SerializeField] Transform swordDownPosition;

	private void Start()
	{
		SetSwordDefaultPosition();
	}

	public void SetSwordDefaultPosition()
	{
		transform.position = swordDownPosition.position;
		transform.rotation = swordDownPosition.rotation;
	}
}
