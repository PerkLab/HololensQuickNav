using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteLog : MonoBehaviour {

    static StreamWriter sw;
    static private string lastString;

    // Use this for initialization
    void Awake () {

        sw = File.CreateText(Application.persistentDataPath+"/QuickNavLogData.txt");
        sw.AutoFlush = true;
        sw.WriteLine("QuickNav Log Data");
        sw.Write("Time of Test: ");
        sw.WriteLine(DateTime.Now);
        sw.WriteLine("-----------------------------");
    }

    public static void WriteData(string Data)
    {
        if(Data != lastString)
        {
            string minutes = Mathf.Floor(Time.timeSinceLevelLoad / 60f).ToString("00");
            string seconds = (Time.timeSinceLevelLoad % 60f).ToString("00");
            sw.Write("Time Elapsed: " + minutes + ":" + seconds);
            sw.WriteLine("   " + Data);
        }
        else
        {
            //don't print command if the same as the last one
        }

        lastString = Data;
        
        
    }

    public void SavePosition()
    {
        GameObject Model = GameObject.Find("Head");
        GameObject GroundTruth = GameObject.Find("GroundTruth").transform.FindChild("Marker").gameObject;
        Vector3 direction = new Vector3(Model.transform.position.x - GroundTruth.transform.position.x, Model.transform.position.y - GroundTruth.transform.position.y, Model.transform.position.z - GroundTruth.transform.position.z);
        sw.WriteLine("-----------------------------");
        string minutes = Mathf.Floor(Time.timeSinceLevelLoad / 60f).ToString("00");
        string seconds = (Time.timeSinceLevelLoad % 60f).ToString("00");
        sw.WriteLine("Time Elapsed: " + minutes + ":" + seconds);
        sw.WriteLine("Model Position Data:");
        sw.WriteLine("X: " + direction.x);
        sw.WriteLine("Y: " + direction.y);
        sw.WriteLine("Z: " + direction.z);
        sw.WriteLine("Model Rotation Data:");
        sw.WriteLine("X: " + Model.transform.localRotation.eulerAngles.x);
        sw.WriteLine("Y: " + Model.transform.localRotation.eulerAngles.y);
        sw.WriteLine("Z: " + Model.transform.localRotation.eulerAngles.z);
        sw.WriteLine("-----------------------------");
    }

}
