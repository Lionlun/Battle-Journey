using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    void Awake()
    {
		Application.targetFrameRate = 60;
	}

}
