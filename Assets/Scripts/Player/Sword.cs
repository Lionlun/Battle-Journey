using System.Threading.Tasks;
using UnityEngine;

public class Sword : MonoBehaviour
{
	[HideInInspector] public bool IsUp { get; set; }
	[HideInInspector] public bool IsStuck { get; set; }

	[SerializeField] private Transform swordAttackPosition;
	[SerializeField] private Transform swordUpPosition;
	[SerializeField] private PlayerController player;

	private int damage = 25;
	private float minimumSpeedToPenetrate = 6;
	private float knockbackCooldown = 0;
	private float knockbackCooldownRefresh = 0.5f;
	private float timeToToggleWeapon;
	private float timeToToggleWeaponRefresh = 1f;

	private bool isHoldingStill;

	private void OnEnable()
	{
		InputEvents.OnHold += CheckHoldStill;
		InputEvents.OnEndTouch += RefreshWeaponTimer;
		IsUp = true;
		transform.localPosition = swordUpPosition.localPosition;
		transform.localRotation = swordUpPosition.localRotation;
	}
	private void OnDisable()
	{
		InputEvents.OnHold -= CheckHoldStill;
		InputEvents.OnEndTouch -= RefreshWeaponTimer;
	}
	private void Start()
	{
		timeToToggleWeapon = timeToToggleWeaponRefresh;
	}
	
	private void Update()
	{
		if (isHoldingStill)
		{
			timeToToggleWeapon -= Time.deltaTime;
		}
		ToggleWeapon();

		if (knockbackCooldown > 0)
		{
			knockbackCooldown -= Time.deltaTime;
		}

		if (IsUp)
		{
			transform.localPosition = swordUpPosition.localPosition;
			transform.localRotation = swordUpPosition.localRotation;
		}
		else
		{
			transform.localPosition = swordAttackPosition.localPosition;
			transform.localRotation = swordAttackPosition.localRotation;
		}
	}

	public void Stuck(Transform enemyTransform)
	{

		var direction = (enemyTransform.position - transform.position).normalized;
		var directionNeeded = new Vector3(direction.x, 0, direction.z);

		transform.position += directionNeeded / 1.5f;

		player.FreezePlayer();
	}

	public void SetIsStuckFalse()
	{
		IsStuck = false;
	}

	public async void Unstuck()
	{
		player.MoveBack();
		await Task.Delay(300);
		player.UnfreezePlayer();
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
						Stuck(other.gameObject.transform);
						enemyHealth.TakeDamage(damage);


					}
				}
			}

			if (other.gameObject.tag == "Wall")
			{
				if (player.Rb.velocity.magnitude > minimumSpeedToPenetrate && !player.IsUnstucking)
				{
					IsStuck = true;
					Stuck(other.gameObject.transform);
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
					knockBackComponent.KnockBack(other.transform.position, 25);
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
