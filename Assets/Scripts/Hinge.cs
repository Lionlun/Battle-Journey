
using UnityEngine;

public class Hinge: MonoBehaviour
{
	public bool IsActive { get; set; }

    public void Activate()
    {
		IsActive = true;
		gameObject.SetActive(true);
    }
	public void Deactivate()
	{
		IsActive = false;
		gameObject.SetActive(false);
	}
}
