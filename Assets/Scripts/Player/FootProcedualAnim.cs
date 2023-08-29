using Unity.VisualScripting;
using UnityEngine;

public class FootProcedualAnim : MonoBehaviour
{
	[SerializeField] Transform body;
	[SerializeField] float footSpacing = 0.2f;
	[SerializeField] float footForwardOffset = 0.5f;
	[SerializeField] LayerMask terrainLayer;
	[SerializeField] float stepDistance = 1;
	[SerializeField] float stepHeight = 1;
	[SerializeField] float speed = 2;
	Vector3 newPosition;
	Vector3 oldPosition;
	Vector3 currentPosition;
	float lerp;
	[SerializeField] bool isLeft;
	
	private void Start()
	{
		currentPosition = transform.position;
		newPosition = transform.position;
		oldPosition = transform.position;
	   Ray ray = new Ray(body.position + (body.right * footSpacing) + body.up*footForwardOffset, Vector3.down);
	

		if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
		{
			newPosition = info.point;
			oldPosition = info.point;
		}
	}
	void Update()
    {
		transform.position = currentPosition;

		Ray ray = new Ray(body.position + (body.right * footSpacing) + body.up * footForwardOffset, Vector3.down);
	
		if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
		{
			if (Vector3.Distance(newPosition, info.point) > stepDistance)
			{
				Debug.Log("Greater");
				lerp = 0;
				newPosition = info.point;
				isLeft = !isLeft;
			}
			
		}
		if (lerp < 1)
		{
			Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
			footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

			if (isLeft)
			{
				currentPosition = footPosition;
			}
				
			lerp += Time.deltaTime *speed;
		}
		else
		{
			
			oldPosition = newPosition;
		
		}
		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(newPosition, 0.5f);
	}
}
