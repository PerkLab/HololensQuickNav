using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LocationValues : MonoBehaviour
{
    private ReadXml xml;

    //use fiducial locations from Slicer to declare static variables for use throughout the entire program
    private Vector3[] fiducials;
    private int numRegFiducials = 0;

    private Vector3[] targetValues;
    private int numTargets = 0;


    //---------------------------------------------------------
    //set values 

    public void clearValues()
    {
        fiducials = null;
        //targets = null;
    }

    public void readXMLPoints(DirectoryInfo newDir)
    {
        xml = GameObject.Find("InputManager/FileBrowser").GetComponent<ReadXml>();
        xml.readXML(newDir);
        List<Vector3> points = xml.getListOfPoints();
        
        fiducials = new Vector3[points.Count];
        for(int i=0;i<points.Count;i++)
        {
            fiducials[i] = new Vector3(points[i].x*0.001f, points[i].y * 0.001f, points[i].z * 0.001f);
        }

        List<Vector3> targets = xml.getListOfTargets();

        targetValues = new Vector3[targets.Count];
        for (int i = 0; i < targets.Count; i++)
        {
            targetValues[i] = new Vector3(targets[i].x * 0.001f, targets[i].y * 0.001f, targets[i].z * 0.001f);
        }
        WriteLog.WriteData("locationvalues - " + targetValues.ToString());
    }


    //---------------------------------------------------------
    //get values 

    public Vector3[] getFiducials()
    {
        return fiducials;
    }

    public Vector3[] getTargets()
    {
        return targetValues;
    }
}
