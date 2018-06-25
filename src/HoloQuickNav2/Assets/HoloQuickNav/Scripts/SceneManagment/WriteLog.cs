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

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("ClickAudio").gameObject);

        //append existing text file or create new file 
        sw = File.AppendText(Application.persistentDataPath + "/QuickNavLogData.txt");
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
            sw.Write(minutes + ":" + seconds);
            sw.WriteLine("," + Data);
        }
        else
        {
            //don't print command if the same as the last one
        }

        lastString = Data;
        
        
    }

}
