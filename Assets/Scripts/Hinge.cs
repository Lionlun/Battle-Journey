
using UnityEngine;

public class Hinge: MonoBehaviour
{
    public void Activate()
    {
        gameObject.SetActive(true);
    }
	public void Deactivate()
	{
		gameObject.SetActive(false);
	}
}
