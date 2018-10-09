using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.Events;

public class GazeInput : MonoBehaviour, IFocusable {

    [Tooltip("time to hold gaze on object before invoking event")]
    public float timeRequiredToInvoke = 2f;
    public UnityEvent onGaze;
    private float timePassed = 0f;
    private bool isRunning = false;
	
	// Update is called once per frame
	void Update () {
        if(isRunning)
        {
            timePassed += Time.deltaTime;
            if (timePassed > timeRequiredToInvoke)
            {
                onGaze.Invoke();
                isRunning = false;
            }
        }
    }

    public void OnFocusEnter()
    {
        timePassed = 0;
        isRunning = true;
    }

    public void OnFocusExit()
    {
        isRunning = false;
    }
}
