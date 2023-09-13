using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAttack : MonoBehaviour
{
	private Rigidbody rb;
	public bool IsAttacking { get; set; }
	private float attackDashSpeed = 25;

	private void OnEnable()
	{
		InputEvents.OnSwipe += Attack;
	}
	private void OnDisable()
	{
		InputEvents.OnSwipe -= Attack;
	}
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private async void Attack(SwipeData data)
	{
		IsAttacking = true;
		var scaledStart = new Vector2(data.StartPosition.x / Screen.width, data.StartPosition.y / Screen.height); //TODO возможно заскейлить distance раньше
		var scaledEnd = new Vector2(data.EndPosition.x / Screen.width, data.EndPosition.y / Screen.height);
		var distance = Vector2.Distance(scaledStart, scaledEnd);

		Vector3 velocity = transform.forward * attackDashSpeed;
		rb.velocity = velocity;

		await Task.Delay(300);
		IsAttacking = false;
	}
}
