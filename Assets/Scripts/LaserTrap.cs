using UnityEngine;

public class LaserTrap : MonoBehaviour
{
	[SerializeField] private Laser laser;

    private bool isMovingRight;
	private float cooldown = 0;


	private void Start()
	{
		laser = GetComponentInChildren<Laser>();
	}
	private void FixedUpdate()
    {
		RotateLaserTowardsDistantWall();
		cooldown -= Time.deltaTime;
        Move();
	}

    private void Move()
    {
        if(isMovingRight)
        {
			transform.position += transform.right/10;
		}
        else
        {
            transform.position -= transform.right/10;
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Wall>() != null && cooldown<0)
		{
			isMovingRight = !isMovingRight;
			cooldown = 2;
		}
	}

	private void RotateLaserTowardsDistantWall()
	{
		if (laser.GetDistanceToWall() < 2)
		{
			transform.RotateAround(transform.position, transform.up, 180f);
		}
	}
}
