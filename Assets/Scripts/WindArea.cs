using UnityEngine;

public class WindArea : MonoBehaviour
{	
	[SerializeField] private float strength;

	public Vector3 GetDirectionForce()
	{
		return transform.right * strength;
	}
}
