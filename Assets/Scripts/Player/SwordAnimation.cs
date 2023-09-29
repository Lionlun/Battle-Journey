using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    
	private Sword sword;
	private Animator animator;

	private void Start()
	{
		animator = GetComponentInChildren<Animator>();
		sword = GetComponentInParent<Sword>();
	}

	public void PlayAttackAnim()
	{
		sword.PlayAttackAnim();
		animator.SetTrigger("Attack");
	}

	public void PlayStuckAnim()
	{
		animator.SetBool("IsStuck", true);
	}

	public void StopStuckAnim()
	{
		animator.SetBool("IsStuck", false);
	}

	public void FinishAttack()
	{
		sword.FinishAttack();
	}
}
