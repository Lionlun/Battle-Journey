using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	Camera cam;

	private void Start()
	{
		cam = Camera.main;
	}

	private void LateUpdate()
	{
		transform.LookAt(cam.transform);
		transform.Rotate(0, 180, 0);
	}
}
