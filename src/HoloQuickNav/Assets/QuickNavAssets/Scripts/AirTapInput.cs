using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
//using HoloToolkit.Unity;
//using UnityEngine.Windows.Speech;
//using System.Collections.Generic;
//using HoloToolkit.Unity.InputModule;

public class AirTapInput : MonoBehaviour//, IInputClickHandler
{
    public bool ToggleEnabled = false;
    [Tooltip("On a double tap, the single tap toggle will reset to Tap1")]
    public bool ToggleResetEnabled = false;
    public UnityEvent OnTap1Events;
    [Tooltip("Toggle between Tap1 and Tap2 when Toggle option selected")]
    public UnityEvent OnTap2Events;
    public UnityEvent OnDoubleTapEvents;
    public UnityEvent OnTripleTapEvents;


    private bool toggle = false;
    private float timeBetweenTaps = 0f;
    private bool timerEnabled = false;
    private bool tapOccured = false;
    private bool secondTapOccured = false;
    private bool thirdTapOccured = false;
    private bool thirdTapEnabled = false;
    private float thirdTapDelay = 0f;
    private float startDelay = 0f;

    private bool IsEnabled = false;

    UnityEngine.XR.WSA.Input.GestureRecognizer recognizer;

    private AudioSource Click;

    // Use this for initialization
    void OnEnable()
    {
        IsEnabled = false;
        tapOccured = false;
        secondTapOccured = false;
        thirdTapOccured = false;
        thirdTapEnabled = false;
        startDelay = 0f;
        //enable gesture recognizer to call AirTap on tap event
        recognizer = new UnityEngine.XR.WSA.Input.GestureRecognizer();
        recognizer.TappedEvent += AirTap;
        recognizer.StartCapturingGestures();

        Click = GameObject.Find("ClickAudio").GetComponent<AudioSource>();
        //WriteLog.WriteData("initialize airtap");
    }


    private void AirTap(UnityEngine.XR.WSA.Input.InteractionSourceKind source, int tapCount, Ray headRay)
    {

        if (IsEnabled)
        {
            //WriteLog.WriteData("tap " + gameObject.gameObject);
            Click.Play();
            if (!timerEnabled) //first tap
            {
                //WriteLog.WriteData("first tap since initialization " + gameObject.gameObject);
                timerEnabled = true;
                timeBetweenTaps = 0f;
            }

            if (!thirdTapOccured && !secondTapOccured && !tapOccured) //if cleared on previous tap or double tap, restart timer on current tap
            {
                timeBetweenTaps = 0f;
                //WriteLog.WriteData("first tap " + gameObject.gameObject);
            }

            if (tapOccured && !secondTapOccured) //single tap occurred, never cleared
            {
                secondTapOccured = true;
                //WriteLog.WriteData("second tap " + gameObject.gameObject);
            }
            else if (tapOccured && secondTapOccured && thirdTapEnabled) //double tap occurred, never cleared
            {
                thirdTapOccured = true;
                //WriteLog.WriteData("third tap " + gameObject.gameObject);
            }

            tapOccured = true;
            


        }
    }

    private void Update()
    {
        if (startDelay < 1f) //delay before enabling airtap
        {
            startDelay += Time.deltaTime;
        }
        else
        {
            IsEnabled = true;
        }

        if (thirdTapDelay < 1.5f) //delay before enabling third tap
        {
            thirdTapDelay += Time.deltaTime;
            thirdTapEnabled = false;
        }
        else
        {
            thirdTapEnabled = true;
        }

        timeBetweenTaps += Time.deltaTime;

        if (timerEnabled)
        {
            if (timeBetweenTaps > 0.8f && tapOccured && !secondTapOccured && !thirdTapOccured) //single tap
            {
                if (!toggle)
                {
                    tapOccured = false;
                    secondTapOccured = false;
                    thirdTapOccured = false;

                    thirdTapEnabled = false;
                    thirdTapDelay = 0f;
                    OnTap1Events.Invoke();
                    toggle = true;
                }
                else if (toggle)
                {
                    if (ToggleEnabled)
                    {
                        tapOccured = false;
                        secondTapOccured = false;
                        thirdTapOccured = false;

                        thirdTapEnabled = false;
                        thirdTapDelay = 0f;
                        OnTap2Events.Invoke();
                    }
                    else
                    {
                        tapOccured = false;
                        secondTapOccured = false;
                        thirdTapOccured = false;

                        thirdTapEnabled = false;
                        thirdTapDelay = 0f;
                        OnTap1Events.Invoke();
                    }

                    toggle = false;
                }

            }
            else if (timeBetweenTaps > 1f && tapOccured && secondTapOccured && !thirdTapOccured) //double tap
            {
                tapOccured = false;
                secondTapOccured = false;
                thirdTapOccured = false;

                thirdTapEnabled = false;
                thirdTapDelay = 0f;

                if(ToggleResetEnabled)
                {
                    toggle = false; //reset to Tap1 event
                }
                OnDoubleTapEvents.Invoke();
            }
            else if (timeBetweenTaps > 1.2f && tapOccured && secondTapOccured && thirdTapOccured) //triple tap
            {
                tapOccured = false;
                secondTapOccured = false;
                thirdTapOccured = false;
                thirdTapEnabled = false;
                thirdTapDelay = 0f;
                OnTripleTapEvents.Invoke();

            }
        }

    }

    private void OnDisable()
    {
        tapOccured = false;
        secondTapOccured = false;
        thirdTapOccured = false;

        recognizer.TappedEvent -= AirTap;
        recognizer.StopCapturingGestures();

        //WriteLog.WriteData("tap disabled " + gameObject.gameObject);
    }
}
