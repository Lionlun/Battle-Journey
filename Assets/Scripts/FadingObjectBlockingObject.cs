using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingObjectBlockingObject : MonoBehaviour
{
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private Transform target;
	[SerializeField] private Camera cam;
	[SerializeField] private float FadeAlpha = 0.33f;
	[SerializeField] private bool retainShadows = true;
	[SerializeField] private Vector3 targetPositionOffset = Vector3.up;
	[SerializeField] private float fadeSpeed = 1;

	[Header("Read Only Data")]
	[SerializeField] private List<FadingObject> objectsBlockingView = new List<FadingObject>();
	private Dictionary<FadingObject, Coroutine> runningCoroutines = new Dictionary<FadingObject, Coroutine>();
	private RaycastHit[] hits = new RaycastHit[10];

	private void Start()
	{
		StartCoroutine(CheckForObjects());
	}

	private IEnumerator CheckForObjects()
	{
		while (true)
		{
			int numberOfHits = Physics.RaycastNonAlloc(
				cam.transform.position,
				(target.transform.position + targetPositionOffset - cam.transform.position).normalized,
				hits,
				Vector3.Distance(cam.transform.position, target.transform.position + targetPositionOffset),
				layerMask);

			if (numberOfHits > 0)
			{
				for (int i = 0; i < numberOfHits; i++)
				{
					FadingObject fadingObject = GetFadingObjectFromHit(hits[i]);
					if (fadingObject != null && !objectsBlockingView.Contains(fadingObject))
					{
						if (runningCoroutines.ContainsKey(fadingObject))
						{
							if (runningCoroutines[fadingObject] != null)
							{
								StopCoroutine(runningCoroutines[fadingObject]);
							}
							runningCoroutines.Remove(fadingObject);
						}
						runningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
						objectsBlockingView.Add(fadingObject);
					}
				}
			}
			FadeObjectsNoLongerBeingHit();
			ClearHits();

			yield return null;
		}
	}

	private IEnumerator FadeObjectOut(FadingObject fadingObject) 
	{
		foreach (Material material in fadingObject.Materials)
		{
			material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			material.SetInt("_ZWrite", 0);
			material.SetInt("_Surface", 1);
			material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

			material.SetShaderPassEnabled("DepthOnly", false);
			material.SetShaderPassEnabled("SHADOWCASTER", retainShadows);
			material.SetOverrideTag("RenderType", "Transparent");
			material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
			material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
		}
			float time = 0;

			while (fadingObject.Materials[0].color.a > FadeAlpha)
			{
				foreach(Material mat in fadingObject.Materials)
				{
					if (mat.HasProperty("_Color"))
					{
						mat.color = new Color(
						mat.color.r,
						mat.color.g,
						mat.color.b,
						Mathf.Lerp(fadingObject.InitialAlpha, FadeAlpha, time * fadeSpeed)
						);
					}
				}
				time += Time.deltaTime;
				yield return null;
			}
			

		if (runningCoroutines.ContainsKey(fadingObject))
		{
			StopCoroutine(runningCoroutines[fadingObject]);
			runningCoroutines.Remove(fadingObject);
		}
	}

	private IEnumerator FadeObjectIn(FadingObject fadingObject)
	{
		float time = 0;

		while (fadingObject.Materials[0].color.a < fadingObject.InitialAlpha)
		{
			foreach (Material mat in fadingObject.Materials)
			{
				if (mat.HasProperty("_Color"))
				{
					mat.color = new Color(
						mat.color.r,
						mat.color.g,
						mat.color.b,
						Mathf.Lerp(FadeAlpha, fadingObject.InitialAlpha, time * fadeSpeed)
					);
				}
			}
			time += Time.deltaTime;
			yield return null;
		}
	

		foreach (Material material in fadingObject.Materials)
		{
			material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			material.SetInt("_ZWrite", 1);
			material.SetInt("_Surface", 0);
			material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

			material.SetShaderPassEnabled("DepthOnly", true);
			material.SetShaderPassEnabled("SHADOWCASTER", true);
			material.SetOverrideTag("RenderType", "Opaque");
			material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
			material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		}

		if (runningCoroutines.ContainsKey(fadingObject))
		{
			StopCoroutine(runningCoroutines[fadingObject]);
			runningCoroutines.Remove(fadingObject);
		}
	}


	private void FadeObjectsNoLongerBeingHit()
	{
		List<FadingObject> objectsToRemove = new List<FadingObject>(objectsBlockingView.Count);
		foreach (FadingObject obj in objectsBlockingView)
		{
			bool objectIsBeingHit = false;
			for (int i = 0; i < hits.Length; i++)
			{
				FadingObject hitFadingObject = GetFadingObjectFromHit(hits[i]);

				if (hitFadingObject != null && obj == hitFadingObject)
				{
					objectIsBeingHit = true;
					break;
				}
			}

			if (!objectIsBeingHit)
			{
				if (runningCoroutines.ContainsKey(obj))
				{
					if (runningCoroutines[obj] != null)
					{
						StopCoroutine(runningCoroutines[obj]);
					}
					runningCoroutines.Remove(obj);
				}
				runningCoroutines.Add(obj, StartCoroutine(FadeObjectIn(obj)));
				objectsToRemove.Add(obj);
			}
		}
		
		foreach (FadingObject removeObject in objectsToRemove)
		{
			objectsBlockingView.Remove(removeObject);
		}
	}

	private FadingObject GetFadingObjectFromHit(RaycastHit hit)
	{
		return hit.collider != null ? hit.collider.GetComponent<FadingObject>() : null;
	}

	private void ClearHits()
	{
		System.Array.Clear(hits, 0, hits.Length);
	}

	public enum FadeMode
	{
		Transparent,
		Fade
	}
}
