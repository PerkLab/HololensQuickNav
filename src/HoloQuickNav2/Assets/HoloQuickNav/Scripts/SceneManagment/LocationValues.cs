using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Attach to "LoadedModels" to ensure it loads in whichever procdure/scene is selected. 
/// Used to keep track of all points and target coordinates read from the patient's .xml
/// </summary>

public class LocationValues : MonoBehaviour
{
    private ReadXml xml;

    /// <summary> Array of fiducial coordinates (eg. tip of nose, corners of eyes)  </summary>
    private Vector3[] fiducials;
    /// <summary> Number of fiducials used for registration and setup  </summary>
    private int numRegFiducials = 0;
    /// <summary> Array of target coordinates for ROI or drill locations </summary>
    private Vector3[] targets;
    /// <summary> Number of targets in model </summary>
    private int numTargets = 0;


    /// <summary>
    /// Clear any existing values from arrays 
    /// </summary>
    private void clearValues()
    {
        fiducials = null;
        targets = null;
    }

    /// <summary>
    /// Load location values from .xml in selected patient directory
    /// </summary>
    /// <param name="patientDir"></param>
    public void readXMLPoints(DirectoryInfo patientDir)
    {
        clearValues();
        //access .xml through the ReadXml component in the file browser
        //this component parses and sorts all contents of the .xml (models/fiducials/targets)
        xml = GameObject.Find("InputManager/QuickNavManager/FileBrowser").GetComponent<ReadXml>();
        xml.readXML(patientDir);

        //recieve list of fiducial points from xml reader and sort into array of vectors
        List<Vector3> points = xml.getListOfPoints();
        fiducials = new Vector3[points.Count];
        for(int i=0;i<points.Count;i++)
        {
            //apply scaling to convert from Slicer (mm) to Unity (m)
            fiducials[i] = new Vector3(points[i].x*0.001f, points[i].y * 0.001f, points[i].z * 0.001f);
        }

        //recieve list of targets from xml reader and sort into array of vectors
        List<Vector3> targets = xml.getListOfTargets();
        this.targets = new Vector3[targets.Count];
        for (int i = 0; i < targets.Count; i++)
        {
            //apply scaling to convert from Slicer (mm) to Unity (m)
            this.targets[i] = new Vector3(targets[i].x * 0.001f, targets[i].y * 0.001f, targets[i].z * 0.001f);
        }
    }


    //--------------------------------------------------------------------------------------
    //Accessors 
    public Vector3[] getFiducials()
    {
        return fiducials;
    }

    public Vector3[] getTargets()
    {
        return targets;
    }
}
