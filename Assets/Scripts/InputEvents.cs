using System;
using UnityEngine;

public class InputEvents
{
	public static event Action OnDoubleTap = delegate { };
	public static event Action OnTap = delegate { };
	public static event Action<SwipeData> OnSwipe = delegate { };
	public static event Action<Vector3> OnTouchPosition = delegate { };
	public static event Action OnTouch = delegate { };
	public static event Action OnEndTouch = delegate { };
	public static event Action<bool> OnHold = delegate { };

	public static void SendDoubleTap()
	{
		OnDoubleTap?.Invoke();
	}

	public static void SendSingleTap()
	{
		OnTap?.Invoke();
	}

	public static void SendSwipe(SwipeData swipeData)
	{
		OnSwipe?.Invoke(swipeData);
	}

	public static void SendTouchPosition(Vector3 position)
	{
		OnTouchPosition?.Invoke(position);
	}

	public static void SendHold(bool isHolding)
	{
		OnHold?.Invoke(isHolding);
	}

	public static void SendTouch()
	{
		OnTouch?.Invoke();
	}

	public static void SendEndTouch()
	{
		OnEndTouch?.Invoke();
	}
}
