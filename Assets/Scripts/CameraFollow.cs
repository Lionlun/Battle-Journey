using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform target;

	private float smoothTime = 0.25f;
    Vector3 velocity = Vector3.zero;

    void Update()
    {
        Follow();
    }

    void Follow()
    {
		if (target != null)
		{
			Vector3 targetPosition = target.position;
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		}
	}
}
