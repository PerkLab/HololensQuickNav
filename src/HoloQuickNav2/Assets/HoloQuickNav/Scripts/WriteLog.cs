using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteLog : MonoBehaviour {

    static StreamWriter sw;
    static private string lastString;
    static public int ID;

    // Use this for initialization
    void Awake () {

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("ClickAudio").gameObject);

        if(!PlayerPrefs.HasKey("ID")) //doesn't exist
        {
            ID = 1;
        }
        else
        {
            ID = PlayerPrefs.GetInt("ID");
            ID++; //increase user ID 
        }
        PlayerPrefs.SetInt("ID", ID);
        PlayerPrefs.Save();

        //copy over any existing text file
        sw = File.AppendText(Application.persistentDataPath + "/QuickNavLogData.txt");
        //sw = File.CreateText(Application.persistentDataPath + "/QuickNavLogData.txt");
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
            sw.Write(ID.ToString() +","+ minutes + ":" + seconds);
            sw.WriteLine("," + Data);
        }
        else
        {
            //don't print command if the same as the last one
        }

        lastString = Data;
        
        
    }

    public static void SavePosition()
    {
        GameObject Model = GameObject.Find("Head");
        GameObject GroundTruth = GameObject.Find("GroundTruth").transform.Find("Marker").gameObject;
        Vector3 directionToModel = Model.transform.position - GroundTruth.transform.position;
        GameObject BurrHole = GameObject.Find("Head").transform.Find("Model/BurrHoleMarker").gameObject;
        Vector3 directionToBurrHole = BurrHole.transform.position - GroundTruth.transform.position;
        sw.WriteLine("-----------------------------");
        string minutes = Mathf.Floor(Time.timeSinceLevelLoad / 60f).ToString("00");
        string seconds = (Time.timeSinceLevelLoad % 60f).ToString("00");
        sw.WriteLine("Time Elapsed: " + minutes + ":" + seconds);
        sw.WriteLine("Model Position Data:");
        sw.WriteLine("X: " + directionToModel.x);
        sw.WriteLine("Y: " + directionToModel.y);
        sw.WriteLine("Z: " + directionToModel.z);
        sw.WriteLine("Model Rotation Data:");
        sw.WriteLine("X: " + Model.transform.localRotation.eulerAngles.x);
        sw.WriteLine("Y: " + Model.transform.localRotation.eulerAngles.y);
        sw.WriteLine("Z: " + Model.transform.localRotation.eulerAngles.z);
        sw.WriteLine("BurrHole Position Data:");
        sw.WriteLine("X: " + directionToBurrHole.x);
        sw.WriteLine("Y: " + directionToBurrHole.y);
        sw.WriteLine("Z: " + directionToBurrHole.z);
        sw.WriteLine("-----------------------------");
    }

    public void ResetID()
    {

        PlayerPrefs.SetInt("ID", 1);
        ID = PlayerPrefs.GetInt("ID");
        PlayerPrefs.Save();
    }

}
