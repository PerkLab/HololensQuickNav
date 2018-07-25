using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteLog : MonoBehaviour {


    static StreamWriter sw;
    /// <summary>
    /// Last string written to file, to check for duplicates if a tool is called twice by accident
    /// </summary>
    static private string lastString;

    void Awake () {

        //don't destroy WriteLog component when switching to other scenes
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
        //if the string to be written is not a duplicate
        if(Data != lastString)
        {
            //print the time the string is written to the file
            string minutes = Mathf.Floor(Time.timeSinceLevelLoad / 60f).ToString("00");
            string seconds = (Time.timeSinceLevelLoad % 60f).ToString("00");
            sw.Write(minutes + ":" + seconds);
            sw.WriteLine("," + Data);
        }

        //update lastString to current string
        lastString = Data;
        
    }

}
