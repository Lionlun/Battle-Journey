using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FloorFlash : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Color originalColor;
	private float numberOfFlashes = 3;
    private int flashTime = 500;

	private void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
	}
	private void Update()
	{
		if (meshRenderer == null)
		{
			
		}
	}
	public async Task Flash()
    {
		for (int i = 0; i < numberOfFlashes; i++)
		{
			meshRenderer.material.color = Color.red;
			await Task.Delay(flashTime);
			if (meshRenderer != null)
			{
				meshRenderer.material.color = originalColor;
			}
			await Task.Delay(flashTime);
		}
	}

	public async Task ActivateTrap()
	{
		await Flash();
		this.gameObject.SetActive(false);
	}

	public void ResetTrap()
	{
		if (this.gameObject != null)
		{
			this.gameObject.SetActive(true);
		}
	}
}
