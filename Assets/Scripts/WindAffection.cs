
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WindAffection : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField] private WindArea windZone;
	
	public bool IsInWindZone { get; private set; } = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (IsInWindZone)
		{
			rb.AddForce(windZone.GetDirectionForce());
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<WindArea>() != null)
		{
			windZone = other.GetComponent<WindArea>();
			IsInWindZone = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.GetComponent<WindArea>() != null)
		{
			IsInWindZone = false;
		}
	}
}
