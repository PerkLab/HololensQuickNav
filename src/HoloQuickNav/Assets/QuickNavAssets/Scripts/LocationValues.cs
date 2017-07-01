using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationValues : MonoBehaviour {

    public TextAsset csvMarkers;
    private char lineSeperator = '\n';
    private char fieldSeperator = ',';

    string[] Nose;
    string[] REye;
    string[] LEye;

    //use fiducial locations from Slicer to declare static variables for use throughout the entire program
    public static float NoseR;
    public static float NoseA;
    public static float NoseS;

    public static float REyeR;
    public static float REyeA;
    public static float REyeS;

    public static float LEyeR;
    public static float LEyeA;
    public static float LEyeS;

    void Awake()
    {
        readData();
    }

    void readData()
    {
        string[] records = csvMarkers.text.Split(lineSeperator);
        int count = 0;
        foreach (string record in records)
        {
            if (count < 3) //first 3 lines in file from Slicer are not needed
            { } //do nothing  
            else
            {
                if(count==3) //line 3 is for the nose
                {
                    Nose = record.Split(fieldSeperator);
                }
                else if(count==4) //line 4 is for the right eye
                {
                    REye = record.Split(fieldSeperator);
                }
                else if(count==5) //line 5 is for the left eye
                {
                    LEye = record.Split(fieldSeperator);
                }
            }
            count++;
        }

        NoseR = float.Parse(Nose[1]) * 0.001f * -1f;
        NoseA = float.Parse(Nose[2]) * 0.001f;
        NoseS = float.Parse(Nose[3]) * 0.001f;

        REyeR = float.Parse(REye[1]) * 0.001f * -1f;
        REyeA = float.Parse(REye[2]) * 0.001f;
        REyeS = float.Parse(REye[3]) * 0.001f;

        LEyeR = float.Parse(LEye[1]) * 0.001f * -1f;
        LEyeA = float.Parse(LEye[2]) * 0.001f;
        LEyeS = float.Parse(LEye[3]) * 0.001f;

    }
}
