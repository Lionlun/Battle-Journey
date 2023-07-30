using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    FieldOfView fov;
    float speed = 6;
    float rotationSpeed = 9;

    void Start()
    {
        fov = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
	}

    void Follow()
    {
        if (fov.IsPlayerSeen)
        {
			Vector3 newDirection = Vector3.RotateTowards(transform.forward, fov.DirectionToTarget, rotationSpeed * Time.deltaTime, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDirection);

			transform.position += transform.forward * Time.deltaTime;
        }
    }
}
