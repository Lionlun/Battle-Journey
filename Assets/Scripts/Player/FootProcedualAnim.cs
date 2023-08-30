using UnityEngine;

public class FootProcedualAnim : MonoBehaviour
{
	#region StepParameters
	[Header("Step Parameters")]
	[SerializeField] private float footSpacing = 0.2f;
	[SerializeField] private float footForwardOffset = 0.5f;
	[SerializeField] private float stepDistance = 1;
	[SerializeField] private float stepHeight = 1;
	[SerializeField] private float animDefaultSpeed = 15;
	[SerializeField] private float animSpeedThreshold = 13;
	[SerializeField] private float animHighSpeed = 50;
	private float animSpeed;
	[Space]
	#endregion

	[SerializeField] private LayerMask terrainLayer;
	[SerializeField] private FootProcedualAnim oppositeFoot;
	[SerializeField] private Transform body;
	[SerializeField] private Rigidbody playerRb;

	private float lerp;
	private Vector3 newPosition;
	private Vector3 oldPosition;
	private Vector3 currentPosition;

	private void Start()
	{
		animSpeed = animDefaultSpeed;
		currentPosition = transform.position;
		currentPosition = newPosition = oldPosition = transform.position;
		lerp = 1;
	}

	private void Update()
	{
		transform.position = currentPosition;
		CheckDistance();
		PlaceLegsDefault();
		MakeStep();
		AccelerateAnimationSpeed();
	}

	public bool IsMoving()
	{
		return lerp < 1;
	}

	private void CheckDistance()
	{
		Ray ray = new Ray(body.position + (body.right * footSpacing) + body.up * footForwardOffset, Vector3.down);

		if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
		{
			if (Vector3.Distance(newPosition, info.point) > stepDistance && !oppositeFoot.IsMoving() && lerp >= 1)
			{
				lerp = 0f;
				newPosition = info.point;
			}
		}
	}

	private void PlaceLegsDefault()
	{
		Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);

		if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
		{
			if (playerRb.velocity == Vector3.zero)
			{
				currentPosition = oldPosition = newPosition = info.point;
			}
		}
	}

	private void MakeStep()
	{
		if (lerp < 1)
		{
			Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
			tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

			currentPosition = tempPosition;
			lerp += Time.deltaTime * animSpeed;
		}
		else
		{
			oldPosition = newPosition;
		}
	}

	private void AccelerateAnimationSpeed()
	{
		if (playerRb.velocity.magnitude > animSpeedThreshold)
		{
			Debug.Log(playerRb.velocity.magnitude);
			animSpeed = animHighSpeed;
		}
		else
		{
			animSpeed = animDefaultSpeed;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(newPosition, 0.1f);
	}
}
