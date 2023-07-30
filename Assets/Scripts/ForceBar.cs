using UnityEngine;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour
{
    public Slider slider;

    public void UpdateForce(float force)
    {
        slider.value = force;
    }
}
