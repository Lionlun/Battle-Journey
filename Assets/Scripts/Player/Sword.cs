using System.Threading.Tasks;
using UnityEngine;

public class Sword : MonoBehaviour
{
	public bool IsUp { get; set; }
	public bool IsStuck { get; set; }
	public bool IsWallStuck { get; set; }
	public bool IsEnemyStuck { get; set; }

	[SerializeField] private PlayerController player;
	[SerializeField] private PlayerWallJump playerJump;

	private int damage = 25;
	private float minimumSpeedToPenetrate = 6;
	private float knockbackCooldown = 0;
	private float knockbackCooldownRefresh = 0.5f;
	private float timeToToggleWeapon;
	private float timeToToggleWeaponRefresh = 1f;

	private bool isHoldingStill;

	[SerializeField] Transform swordUpPosition;
	[SerializeField] Transform swordDownPosition;
	[SerializeField] Transform weaponPoint;
	Vector3 positionAtCollision = Vector3.zero;

	private void OnEnable()
	{
		InputEvents.OnHold += CheckHoldStill;
		InputEvents.OnEndTouch += RefreshWeaponTimer;
	}
	private void OnDisable()
	{
		InputEvents.OnHold -= CheckHoldStill;
		InputEvents.OnEndTouch -= RefreshWeaponTimer;
	}
	private void Start()
	{
		timeToToggleWeapon = timeToToggleWeaponRefresh;
		IsUp = true;
	}
	
	private void Update()
	{
		if (!IsStuck)
		{
			transform.position = weaponPoint.position;
			transform.rotation = weaponPoint.rotation;
		}

		if (isHoldingStill)
		{
			timeToToggleWeapon -= Time.deltaTime;
		}
		ToggleWeapon();

		if (knockbackCooldown > 0)
		{
			knockbackCooldown -= Time.deltaTime;
		}
		if (IsStuck)
		{
			transform.position = positionAtCollision; //в процессе
		}
		if (IsUp && !IsStuck)
		{
			weaponPoint.position = swordUpPosition.position;
			weaponPoint.rotation = swordUpPosition.rotation;
		}
		if (!IsUp && !IsStuck)
		{
			weaponPoint.position = swordDownPosition.position;
			weaponPoint.rotation = swordDownPosition.rotation;
		}
	}

	public void SetIsStuckFalse()
	{
		IsStuck = false;
	}

	public void Unstuck()
	{
		player.MoveBack();
	}

	private void ObjectStuck<T>(T type)
	{
		if (type is Enemy)
		{
			Debug.Log("IS ENEMY");
			IsWallStuck = false;
			IsEnemyStuck = true;
		}
		else if (type is Wall)
		{
			Debug.Log("IS WALL");
			IsWallStuck = true;
			IsEnemyStuck = false;
		}

		if (!player.IsUnstucking)
		{
			positionAtCollision = transform.position; //в процессе
			IsStuck = true;
		}
	}


	private async void OnTriggerEnter(Collider other)
	{
		if (!IsUp)
		{
			if (other.gameObject.GetComponent<Enemy>() != null)
			{
				var enemy = other.gameObject.GetComponent<Enemy>();

				if (player.Rb.velocity.magnitude > minimumSpeedToPenetrate)
				{
					var enemyHealth = other.gameObject.GetComponent<Health>();

					await Task.Delay(100);
					
					if (other != null)
					{
						ObjectStuck(enemy);
					
						enemyHealth.TakeDamage(damage);
					}
				}
			}

			if (other.gameObject.GetComponent<Wall>() != null)
			{
				var wall = other.gameObject.GetComponent<Wall>();
				if (player.Rb.velocity.magnitude > minimumSpeedToPenetrate && !player.IsUnstucking)
				{
					ObjectStuck(wall);
				}
			}
		}
	}


	private void OnTriggerStay(Collider other)
	{
		if (!IsUp)
		{
			if (other.gameObject.GetComponent<Enemy>() != null)
			{
				if (player.Rb.velocity.magnitude < minimumSpeedToPenetrate && knockbackCooldown <=0 && !IsStuck)
				{
					knockbackCooldown = knockbackCooldownRefresh;
					var knockBackComponent = player.gameObject.GetComponent<Knockback>();
					knockBackComponent.KnockBack(other.transform.position, 250);
				}
			}

			if (other.gameObject.GetComponent<Wall>() != null)
			{
				if (player.Rb.velocity.magnitude < minimumSpeedToPenetrate)
				{
					var knockBackComponent = player.gameObject.GetComponent<Knockback>();
					//knockBackComponent.KnockBack(other.transform.position, 25);
				}
			}
		}
	}

	private void CheckHoldStill(bool isHolding)
	{
		isHoldingStill = isHolding;
	}

	private void ToggleWeapon()
	{
		if (timeToToggleWeapon <= 0)
		{
			IsUp = !IsUp;
			timeToToggleWeapon = timeToToggleWeaponRefresh;
		}
	}
	private void RefreshWeaponTimer()
	{
		timeToToggleWeapon = timeToToggleWeaponRefresh;
	}
}
