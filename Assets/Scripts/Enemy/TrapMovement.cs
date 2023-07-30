using System.Collections;
using UnityEngine;

public class TrapMovement : MonoBehaviour
{
	Rigidbody rb;

	void Start()
    {
		rb = GetComponent<Rigidbody>();
		StartCoroutine(Movement());
	}

	IEnumerator Movement()
	{
		rb.velocity = Vector3.zero;
		rb.AddForce(transform.forward * 14, ForceMode.Impulse);
		yield return new WaitForSeconds(1.5f);
		rb.velocity = Vector3.zero;
		rb.AddForce(transform.forward * -14, ForceMode.Impulse);
		yield return new WaitForSeconds(1.5f);
		StartCoroutine(Movement());
	}
}
