using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InstructionVideos : MonoBehaviour {

    private GameObject MenuBackground;
    private GameObject MenuButtons;
    private VideoPlayer CurrentVideo;


    private void Start()
    {
        MenuBackground = GameObject.Find("MenuInstructions").transform.FindChild("Background").gameObject;
        MenuButtons = GameObject.Find("MenuInstructions").transform.FindChild("Buttons").gameObject;
        CurrentVideo.loopPointReached += VideoEnded;
    }


    public void Registration()
    {
        WriteLog.WriteData("Instructions > Registration");
        MenuBackground.SetActive(false);
        MenuButtons.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("3PtRegistration").gameObject.SetActive(true);
        CurrentVideo = GameObject.Find("Videos").transform.FindChild("3PtRegistration").GetComponent<VideoPlayer>();
        
    }

    public void Rotate()
    {
        WriteLog.WriteData("Instructions > Rotate");
        MenuBackground.SetActive(false);
        MenuButtons.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Rotate").gameObject.SetActive(true);
        CurrentVideo = GameObject.Find("Videos").transform.FindChild("Rotate").GetComponent<VideoPlayer>();

    }

    public void Move()
    {
        WriteLog.WriteData("Instructions > Move");
        MenuBackground.SetActive(false);
        MenuButtons.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Move").gameObject.SetActive(true);
        CurrentVideo = GameObject.Find("Videos").transform.FindChild("Move").GetComponent<VideoPlayer>();
    }

    public void Pause()
    {
        WriteLog.WriteData("Instructions > Pause");
        CurrentVideo.Pause();
    }

    public void Stop()
    {
        WriteLog.WriteData("Instructions > Stop");
        CurrentVideo.Stop();
        GameObject.Find("Videos").transform.FindChild("3PtRegistration").gameObject.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Rotate").gameObject.SetActive(false);
        GameObject.Find("Videos").transform.FindChild("Move").gameObject.SetActive(false);
        MenuBackground.SetActive(true);
        MenuButtons.SetActive(true);
        
    }

    public void Play()
    {
        WriteLog.WriteData("Instructions > Play");
        CurrentVideo.Play();
    }

    void VideoEnded(UnityEngine.Video.VideoPlayer vp)
    {
        //if video ends return to instructions menu
        Stop();
    }
}
