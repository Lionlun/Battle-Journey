
using UnityEngine;

public class Hinge: MonoBehaviour
{
	public bool IsActive { get; set; }

    public void Activate()
    {
		IsActive = true;
		Debug.Log("Hinge activated");
		gameObject.SetActive(true);
    }
	public void Deactivate()
	{
		Debug.Log("Hinge DEactivated");
		IsActive = false;
		gameObject.SetActive(false);
	}
}
