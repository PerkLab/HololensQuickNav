using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class InstructionVideos : MonoBehaviour {

    private GameObject MenuBackground;
    private GameObject MenuButtons;
    private VideoPlayer CurrentVideo;
    private AudioSource CurrentAudio;
    private bool checkIfPlaying;
    private bool fullVideoPlaying;
    private int videoCount = 0;
    public UnityEvent Swap;



    private void Start()
    {
        checkIfPlaying = false;
        fullVideoPlaying = false;
        MenuBackground = GameObject.Find("MenuInstructions").transform.FindChild("Background").gameObject;
        MenuButtons = GameObject.Find("MenuInstructions").transform.FindChild("Buttons").gameObject;

        if (SwapScene.CommandName == "Done")
        {
            MenuBackground.SetActive(true);
            MenuButtons.SetActive(true);
        }
        else if(SwapScene.CommandName == "Shift")
        {
            Shift();
        }
        else if (SwapScene.CommandName == "Rotate")
        {
            Rotate();
        }
        else if (SwapScene.CommandName == "Move")
        {
            Move();
        }
        else if (SwapScene.CommandName == "3Point")
        {
            Registration();
        }


    }


    private void Update()
    {
        if (checkIfPlaying)
        {
            if (!CurrentVideo.isPlaying) //end reached
            {
                EndReached();
                if(fullVideoPlaying)
                {
                    FullVideo();  //call next video in series
                }
            }
        }
    }

    public void FullVideo()
    {
        WriteLog.WriteData("Instructions - Full Video");
        fullVideoPlaying = true;
        if (videoCount == 0)
        {
            Registration();
            videoCount++;
        }
        else if(videoCount == 1)
        {
            Rotate();
            videoCount++;
        }
        else if(videoCount == 2)
        {
            Move();
            videoCount++;
        }
        else if (videoCount == 3)
        {
            fullVideoPlaying = false;
            Shift();
            videoCount = 0; 
        }
    }

    public void Registration()
    {
        WriteLog.WriteData("Instructions - Registration");
        MenuBackground.SetActive(false);
        MenuButtons.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("3PtRegistration").gameObject.SetActive(true);
        CurrentVideo = GameObject.Find("Videos").transform.FindChild("3PtRegistration").GetComponent<VideoPlayer>();
        CurrentAudio = GameObject.Find("Videos").transform.FindChild("3PtRegistration").GetComponent<AudioSource>();
        checkIfPlaying = true;

    }

    public void Rotate()
    {
        WriteLog.WriteData("Instructions - Rotate");
        MenuBackground.SetActive(false);
        MenuButtons.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Rotate").gameObject.SetActive(true);
        CurrentVideo = GameObject.Find("Videos").transform.FindChild("Rotate").GetComponent<VideoPlayer>();
        CurrentAudio = GameObject.Find("Videos").transform.FindChild("Rotate").GetComponent<AudioSource>();
        checkIfPlaying = true;

    }

    public void Move()
    {
        WriteLog.WriteData("Instructions - Move");
        MenuBackground.SetActive(false);
        MenuButtons.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Move").gameObject.SetActive(true);
        CurrentVideo = GameObject.Find("Videos").transform.FindChild("Move").GetComponent<VideoPlayer>();
        CurrentAudio = GameObject.Find("Videos").transform.FindChild("Move").GetComponent<AudioSource>();
        checkIfPlaying = true;
    }

    public void Shift()
    {
        WriteLog.WriteData("Instructions - Shift");
        MenuBackground.SetActive(false);
        MenuButtons.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Shift").gameObject.SetActive(true);
        CurrentVideo = GameObject.Find("Videos").transform.FindChild("Shift").GetComponent<VideoPlayer>();
        CurrentAudio = GameObject.Find("Videos").transform.FindChild("Shift").GetComponent<AudioSource>();
        checkIfPlaying = true;
    }

    public void Pause()
    {
        WriteLog.WriteData("Instructions - Pause");
        checkIfPlaying = false;
        CurrentVideo.Pause();
        CurrentAudio.Pause();
        
    }

    public void Stop()
    {
        WriteLog.WriteData("Instructions - Stop");
        checkIfPlaying = false;
        fullVideoPlaying = false;
        CurrentVideo.Stop();
        CurrentAudio.Stop();
        GameObject.Find("Videos").transform.FindChild("3PtRegistration").gameObject.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Rotate").gameObject.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Move").gameObject.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Shift").gameObject.SetActive(false);

        if (SwapScene.CommandName == "Done")
        {
            MenuBackground.SetActive(true);
            MenuButtons.SetActive(true);
        }
        else if (SwapScene.CommandName == "Shift")
        {
            Swap.Invoke();
        }
        else if (SwapScene.CommandName == "Rotate")
        {
            Swap.Invoke();
        }
        else if (SwapScene.CommandName == "Move")
        {
            Swap.Invoke();
        }
        else if (SwapScene.CommandName == "3Point")
        {
            Swap.Invoke();
        }

    }

    public void Play()
    {
        WriteLog.WriteData("Instructions - Play");
        CurrentVideo.Play();
        CurrentAudio.UnPause();
        checkIfPlaying = true;
    }

    void EndReached()
    {
        checkIfPlaying = false;
        CurrentVideo.Stop();
        CurrentAudio.Stop();
        GameObject.Find("Videos").transform.FindChild("3PtRegistration").gameObject.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Rotate").gameObject.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Move").gameObject.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Shift").gameObject.SetActive(false);
        if (SwapScene.CommandName == "Done")
        {
            MenuBackground.SetActive(true);
            MenuButtons.SetActive(true);
        }
        else if (SwapScene.CommandName == "Shift")
        {
            Swap.Invoke();
        }
        else if (SwapScene.CommandName == "Rotate")
        {
            Swap.Invoke();
        }
        else if (SwapScene.CommandName == "Move")
        {
            Swap.Invoke();
        }
        else if (SwapScene.CommandName == "3Point")
        {
            Swap.Invoke();
        }
    }

}
