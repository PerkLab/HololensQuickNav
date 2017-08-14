using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationValues : MonoBehaviour
{

    public TextAsset csvMarkers;
    public TextAsset csvBBs;
    public TextAsset csvTarget;
    private char lineSeperator = '\n';
    private char fieldSeperator = ',';

    string[] Nose;
    string[] REye;
    string[] LEye;
    string[] BurrHole;
    string[] Target;

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

    public static float BB1R;
    public static float BB1A;
    public static float BB1S;

    public static float BB2R;
    public static float BB2A;
    public static float BB2S;

    public static float BB3R;
    public static float BB3A;
    public static float BB3S;

    public static float BB4R;
    public static float BB4A;
    public static float BB4S;

    public static float BB5R;
    public static float BB5A;
    public static float BB5S;

    public static float BB6R;
    public static float BB6A;
    public static float BB6S;

    public static float targetR;
    public static float targetA;
    public static float targetS;

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
                if (count == 3) //line 3 is for the nose
                {
                    Nose = record.Split(fieldSeperator);
                }
                else if (count == 4) //line 4 is for the right eye
                {
                    REye = record.Split(fieldSeperator);
                }
                else if (count == 5) //line 5 is for the left eye
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



        records = csvBBs.text.Split(lineSeperator);
        count = 0;
        foreach (string record in records)
        {
            if (count < 3) //first 3 lines in file from Slicer are not needed
            {
            } //do nothing  
            else
            {
                BurrHole = record.Split(fieldSeperator);

                if (count == 3) 
                {
                    BB1R = float.Parse(BurrHole[1]) * 0.001f * -1f;
                    BB1A = float.Parse(BurrHole[2]) * 0.001f;
                    BB1S = float.Parse(BurrHole[3]) * 0.001f;
                }
                else if (count == 4) 
                {
                    BB2R = float.Parse(BurrHole[1]) * 0.001f * -1f;
                    BB2A = float.Parse(BurrHole[2]) * 0.001f;
                    BB2S = float.Parse(BurrHole[3]) * 0.001f;
                }
                else if (count == 5) 
                {
                    BB3R = float.Parse(BurrHole[1]) * 0.001f * -1f;
                    BB3A = float.Parse(BurrHole[2]) * 0.001f;
                    BB3S = float.Parse(BurrHole[3]) * 0.001f;
                }
                else if (count == 6) 
                {
                    BB4R = float.Parse(BurrHole[1]) * 0.001f * -1f;
                    BB4A = float.Parse(BurrHole[2]) * 0.001f;
                    BB4S = float.Parse(BurrHole[3]) * 0.001f;
                }
                else if (count == 7) 
                {
                    BB5R = float.Parse(BurrHole[1]) * 0.001f * -1f;
                    BB5A = float.Parse(BurrHole[2]) * 0.001f;
                    BB5S = float.Parse(BurrHole[3]) * 0.001f;
                }
                else if (count == 8) 
                {
                    BB6R = float.Parse(BurrHole[1]) * 0.001f * -1f;
                    BB6A = float.Parse(BurrHole[2]) * 0.001f;
                    BB6S = float.Parse(BurrHole[3]) * 0.001f;
                }

            }
            count++;

        }

        records = csvTarget.text.Split(lineSeperator);
        count = 0;
        foreach (string record in records)
        {
            if (count < 3) //first 3 lines in file from Slicer are not needed
            { } //do nothing  
            else
            {
                if (count == 3) //line 3 is for the nose
                {
                    Target = record.Split(fieldSeperator);
                }
               
            }
            count++;
        }

        targetR = float.Parse(Target[1]) * 0.001f * -1f;
        targetA = float.Parse(Target[2]) * 0.001f;
        targetS = float.Parse(Target[3]) * 0.001f;

    }
}
