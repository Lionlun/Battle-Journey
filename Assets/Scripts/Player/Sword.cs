using System.Threading.Tasks;
using UnityEngine;

public class Sword : MonoBehaviour
{
	[HideInInspector] public bool IsUp { get; set; }
	[HideInInspector] public bool IsStuck { get; set; }

	[SerializeField] private PlayerController player;

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

	private void WallStuck()
	{
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
			if (other.gameObject.tag == "Enemy")
			{
				if (player.Rb.velocity.magnitude > minimumSpeedToPenetrate)
				{
					var enemyHealth = other.gameObject.GetComponent<Health>();

					await Task.Delay(100);
					
					if (other != null)
					{
						IsStuck = true;

						enemyHealth.TakeDamage(damage);
					}
				}
			}

			if (other.gameObject.tag == "Wall")
			{
				if (player.Rb.velocity.magnitude > minimumSpeedToPenetrate && !player.IsUnstucking)
				{
					WallStuck();
				}
			}
		}
	}


	private void OnTriggerStay(Collider other)
	{
		if (!IsUp)
		{
			if (other.gameObject.tag == "Enemy")
			{
				if (player.Rb.velocity.magnitude < minimumSpeedToPenetrate && knockbackCooldown <=0)
				{
					knockbackCooldown = knockbackCooldownRefresh;
					var enemyHealth = other.gameObject.GetComponent<Health>();
					var knockBackComponent = player.gameObject.GetComponent<Knockback>();
					knockBackComponent.KnockBack(other.transform.position, 250);
				}
			}

			if (other.gameObject.tag == "Wall")
			{
				if (player.Rb.velocity.magnitude < minimumSpeedToPenetrate)
				{
					var enemyHealth = other.gameObject.GetComponent<Health>();
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
