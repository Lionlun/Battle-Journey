using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricSwipeDetection : MonoBehaviour
{
	public delegate void SwipeEvent(Vector3 direction, float duration);
	public event SwipeEvent OnSwipe;

	[SerializeField] float minimumDistance = 1f;
	[SerializeField] float minimumTime = 0.05f;
	[SerializeField, Range(0, 1)] private float directionThreshold = .9f;
	private InputManager inputManager;
	private Vector2 startPosition;
	private float startTime;
	private float distance = 0;
	private Vector2 endPosition;
	private float endTime;
	private float duration;

	Coroutine coroutine;
	Coroutine secondCoroutine;

	[SerializeField] ForceBar forceBar;

	[SerializeField] GameObject trail;

	float nextSwipeCoolDown;
	float nextSwipeCoolDownRefresh = 0.5f;

	[SerializeField] Camera cam;

	private void Awake()
	{
		inputManager = GetComponent<InputManager>();
		nextSwipeCoolDown = nextSwipeCoolDownRefresh;
	}

	private void OnEnable()
	{
		inputManager.OnStartTouch += SwipeStart;
		inputManager.OnEndTouch += SwipeEnd;
	}

	private void OnDisable()
	{
		inputManager.OnStartTouch -= SwipeStart;
		inputManager.OnEndTouch -= SwipeEnd;
	}

	private void Update()
	{
		if (nextSwipeCoolDown > 0)
		{
			nextSwipeCoolDown -= Time.deltaTime;
		}
	}

	void SwipeStart(Vector3 position, float time)
	{
		coroutine = StartCoroutine(MeasureDistance());

		if (nextSwipeCoolDown <= 0)
		{
			startPosition = position;
			startTime = time;
			trail.SetActive(true);
			trail.transform.position = position;
		}
	}

	IEnumerator MakeTrail()
	{
		while (true)
		{
			trail.transform.position = inputManager.PrimaryPosition();
			yield return null;
		}
	}
	IEnumerator MeasureDistance()
	{
		while (true)
		{
			distance = Vector3.Distance(startPosition, inputManager.worldMousePosition);

			if (distance > 5)
			{
				distance = 5;
			}

			//forceBar.UpdateForce(distance);
			yield return null;
		}
	}

	void SwipeEnd(Vector3 position, float time)
	{
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
		}


		if (nextSwipeCoolDown <= 0)
		{
			trail.SetActive(false);

			if (secondCoroutine != null)
			{
				StopCoroutine(secondCoroutine);
			}

			endPosition = position;
			endTime = time;
			DetectSwipe();
			nextSwipeCoolDown = nextSwipeCoolDownRefresh;
		}
	}

	void DetectSwipe()
	{
		if (Vector3.Distance(startPosition, endPosition) >= minimumDistance)
		{
			Debug.DrawLine(startPosition, endPosition, Color.red, 5);
			Vector3 direction = endPosition - startPosition;
		
			Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
			Debug.Log(direction2D);
			SwipeDirection(direction2D, distance);
		}
	}
	void SwipeDirection(Vector2 direction, float distance)
	{
		this.distance = 0;
		//forceBar.UpdateForce(this.distance);
	}
}
