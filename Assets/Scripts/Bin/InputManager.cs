using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	public delegate void StartTouchEvent(Vector3 position, float time);
	public event StartTouchEvent OnStartTouch;
	public delegate void EndTouchEvent(Vector3 position, float time);
	public event EndTouchEvent OnEndTouch;

	private PlayerControls playerControls;

	private Camera mainCamera;

	public Vector3 mousePosition;
	public Vector3 worldMousePosition;
	Vector3 isometricMouse;
	[SerializeField] Camera cam;
	public Vector3 CurrentPosition;

	private void Awake()
	{
	
		playerControls = new PlayerControls();
		mainCamera = Camera.main;
	}

	private void OnEnable()
	{
		playerControls.Enable();
	}
	private void OnDisable()
	{
		playerControls.Disable();
	}

	private void Start()
	{
		playerControls.Touch.PrimaryContact.started += ctx => StartTouch(ctx);
		playerControls.Touch.PrimaryContact.canceled += ctx => EndTouch(ctx);

		//playerControls.Touch.Release.performed += ctx => EndTouch(ctx);
	}
	private void Update()
	{
		DetectCurrentPosition();
		mousePosition = playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
		mousePosition.z = Camera.main.nearClipPlane;
		worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
	}

	private async void StartTouch(InputAction.CallbackContext context)
	{
		if (OnStartTouch != null)
		{
			await Task.Delay(50);
			Debug.Log("Start Touch");
			Vector3 mousePos = Input.mousePosition;
			Vector3 screenPos = cam.WorldToScreenPoint(mousePos);

			//OnStartTouch(Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
			OnStartTouch(Camera.main.ScreenToWorldPoint(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
		}
	}
	private void EndTouch(InputAction.CallbackContext context)
	{
		if (OnEndTouch != null)
		{
			//OnEndTouch(Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
			OnEndTouch(Camera.main.ScreenToWorldPoint(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
		}
	}

	public Vector2 PrimaryPosition()
	{
		return Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
	}

	public void Jump()
	{
		Debug.Log("Jump!");
	}
	void DetectCurrentPosition()
	{
		CurrentPosition = Camera.main.ScreenToWorldPoint(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
	}
}
