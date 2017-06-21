using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Events;
//using HoloToolkit.Unity;
//using UnityEngine.Windows.Speech;
//using System.Collections.Generic;
//using HoloToolkit.Unity.InputModule;

public class AirTapInput : MonoBehaviour//, IInputClickHandler
{
    public bool ToggleEnabled;
    public UnityEvent OnTap1Events;
    [Tooltip("Toggle between Tap1 and Tap2 when Toggle option selected")]
    public UnityEvent OnTap2Events;
    public UnityEvent OnDoubleTapEvents;
    public UnityEvent OnHoldEvents;
    

    private bool toggle = false;
    private float timeBetweenTaps = 0f;
    private float holdTimer = 0f;
    private float holdDelay = 0f;
    private bool holdStarted = false;
    private bool timerEnabled = false;
    private bool tapOccured = false;
    private bool secondTapOccured = false;

    private bool IsEnabled = false;

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start () {

        //enable gesture recognizer to call AirTap on tap event
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += AirTap;
        recognizer.HoldStartedEvent += HoldStart;
        recognizer.StartCapturingGestures();
        IsEnabled = true;
    }

    private void HoldStart(InteractionSourceKind source, Ray headRay)
    {
        if(IsEnabled)
        {
            holdTimer = 0f;
            holdStarted = true;
        }
    }


    private void AirTap(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if(IsEnabled)
        {
            if (!timerEnabled) //first tap
            {
                timerEnabled = true;
            }

            if (!secondTapOccured && !tapOccured) //if cleared on previous tap or double tap, restart timer on current tap
            {
                timeBetweenTaps = 0f;
            }

            if(tapOccured) //already set true from previous tap, never cleared
            {
                secondTapOccured = true;
            }

            tapOccured = true;

                   
        }
    }

    private void Update()
    {
        timeBetweenTaps += Time.deltaTime;
        holdTimer += Time.deltaTime;
        holdDelay += Time.deltaTime;
        if(holdDelay > 2f)
        {
            recognizer.HoldStartedEvent += HoldStart;
        }

        if(timerEnabled)
        {
            if (timeBetweenTaps < 1f && secondTapOccured) //double tap
            {
                OnDoubleTapEvents.Invoke();
                tapOccured = false;
                secondTapOccured = false;
            }
            else if (timeBetweenTaps > 1.1f && tapOccured) //single tap, wait for possible second tap
            {
                if(!toggle)
                {
                    OnTap1Events.Invoke();
                    toggle = true;
                }
                else if(toggle)
                {
                    if(ToggleEnabled)
                    {
                        OnTap2Events.Invoke();
                    }
                    else
                    {
                        OnTap1Events.Invoke();
                    }
                    
                    toggle = false;
                }
                tapOccured = false;
            }
        }

        if(holdTimer > 1f && holdStarted)
        {
            OnHoldEvents.Invoke();
            holdStarted = false;
            holdTimer = 0f;

            //stop recognizing hold gesture for a bit
            holdDelay = 0f;
            recognizer.HoldStartedEvent -= HoldStart;
        }
    }
}
