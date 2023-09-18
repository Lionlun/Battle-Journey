using UnityEngine;

public class UIActivationControl : MonoBehaviour
{
    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
		this.gameObject.SetActive(false);
	}
}
