using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationValues : MonoBehaviour
{

    public TextAsset csvMarkers;
    public TextAsset csvBBs;
    private char lineSeperator = '\n';
    private char fieldSeperator = ',';

    //use fiducial locations from Slicer to declare static variables for use throughout the entire program
    public static Vector3[] fiducials;
    public static int numRegFiducials = 0;

    public static Vector3[] targets;
    public static int numTargets = 0;

    void Awake()
    {
        readData();
    }

    void readData()
    {
        //store registration fiducial points
        string[] records = csvMarkers.text.Split(lineSeperator);
        numRegFiducials = records.Length - 4; //first 3 lines in file from Slicer are not needed and last element in records contains nothing
        fiducials = new Vector3[numRegFiducials];
        for (int i=0; i< numRegFiducials; i++)
        {
            string[] values = records[i + 3].Split(fieldSeperator);
            float valR = float.Parse(values[1]) * 0.001f * -1f;
            float valA = float.Parse(values[2]) * 0.001f;
            float valS = float.Parse(values[3]) * 0.001f;
            fiducials[i] = new Vector3(valR, valA, valS);
        }

        //store target points
        if(csvBBs != null)
        {
            records = csvBBs.text.Split(lineSeperator);
            numTargets = records.Length - 4; //first 3 lines in file from Slicer are not needed
            targets = new Vector3[numTargets];
            for (int i = 0; i < numTargets; i++)
            {
                string[] values = records[i + 3].Split(fieldSeperator);
                float valR = float.Parse(values[1]) * 0.001f * -1f;
                float valA = float.Parse(values[2]) * 0.001f;
                float valS = float.Parse(values[3]) * 0.001f;
                targets[i] = new Vector3(valR, valA, valS);
            }
        }
        

    }
}
