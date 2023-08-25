using System.Threading.Tasks;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
	[SerializeField] private TouchDetection touchDetection;
	[SerializeField] private Sword sword;
	public int NumberOfJumps { get; set; } = 2;
	public bool CanJump { get; set; }
	
	Rigidbody rb;
	PlayerController playerController;

	private void OnEnable()
	{
		InputEvents.OnSwipe += WallJump;
	}
	private void OnDisable()
	{
		InputEvents.OnSwipe -= WallJump;
	}

	private void Start()
	{
		playerController = GetComponent<PlayerController>();
		rb = GetComponent<Rigidbody>();
	}

	private async void WallJump(SwipeData data)
	{
		if (sword.IsStuck)
		{
			playerController.UnfreezePlayer();

			var relative = (transform.position + touchDetection.CurrentDirection.ToIso()) - transform.position;
			var rot = Quaternion.LookRotation(relative, Vector2.up);
			transform.rotation = rot;

			Vector3 velocity = transform.forward * 8;
			rb.velocity = velocity;
			rb.velocity += new Vector3(0, 5, 0);
			
			await Task.Delay(200);
			playerController.UnfreezePlayer();
			sword.SetIsStuckFalse();
		}
	}
}
