using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// Used to display xml information on OpenIGTLink UI. Indicates to user if proper data has been recieved from Slicer and stored.
/// TODO: finish update feature once connection established with Slicer
/// </summary>
public class ModelLoaderMenu : MonoBehaviour {

    ///<summary> File path to directory containing patient data </summary>
    private string filePath;
    ///<summary> Names of assets listed in xml file and expected to be in the patient directory </summary>
    private List<string> assets;
    ///<summary> ReadXml instance to provide data from xml in directory </summary>
    private ReadXml xml;

    /// <summary>
    /// Initialize UI and clear all assets and patient information
    /// </summary>
    private void Awake()
    {
        //initialize filePath
        filePath = Application.persistentDataPath + "/PatientData";

        //clear list of assets
        GameObject.Find("PatientID").GetComponent<TextMesh>().text = "PatientID:";
        GameObject.Find("Procedure").GetComponent<TextMesh>().text = "Procedure:";
        foreach(TextMesh textMesh in GameObject.Find("Assets").GetComponentsInChildren<TextMesh>())
        {
            textMesh.text = "";
        }
        foreach (TextMesh textMesh in GameObject.Find("Status").GetComponentsInChildren<TextMesh>())
        {
            textMesh.text = "";
        }
        //display patient id and procedure as not loaded
        GameObject.Find("Status").transform.GetChild(0).GetComponent<TextMesh>().text = "---";
        GameObject.Find("Status").transform.GetChild(1).GetComponent<TextMesh>().text = "---";

    }

    /// <summary>
    /// Update UI with data from xml to show user what assets are expected from Slicer
    /// 
    /// TODO: currently hardcoded in ReadXml script to access xml from SDH demo, this will need to be changed to read any xml sent from Slicer
    /// 
    /// </summary>
    public void displayXmlData()
    {
        //access xml file
        xml = GameObject.Find("OpenIGTLink").GetComponent<ReadXml>();
        assets = new List<string>();

        //update filePath based on xml
        filePath = filePath + "/" + xml.getProcedureType() + "/" + xml.getPatientID();

        //get patient id and procedure, show that values are loaded on UI
        GameObject.Find("PatientID").GetComponent<TextMesh>().text = "PatientID: " + xml.getPatientID();
        GameObject.Find("Procedure").GetComponent<TextMesh>().text = "Procedure: " + xml.getProcedureType();
        GameObject.Find("Status").transform.GetChild(0).GetComponent<TextMesh>().text = "yes";
        GameObject.Find("Status").transform.GetChild(1).GetComponent<TextMesh>().text = "yes";

        //get full list of asset names
        foreach (string model in xml.getListOfModels())
        {
            assets.Add(model);
        }

        foreach (string point in xml.getListOfPointNames())
        {
            assets.Add(point);
        }

        foreach (string target in xml.getListOfTargetNames())
        {
            assets.Add(target);
        }

        //display asset names on UI
        //for points loaded directly from xml, indicate that they are properly loaded
        for (int i=0;i<assets.Count;i++)
        {
            //set asset names and show if loaded
            GameObject.Find("Assets").transform.GetChild(i).GetComponent<TextMesh>().text = assets[i];
            //if an obj file, show not loaded until recieved over connection
            if (assets[i].Contains(".obj"))
                GameObject.Find("Status").transform.GetChild(i + 2).GetComponent<TextMesh>().text = "---";   //first two children in status object display status for patient id and procedure type
            else
                GameObject.Find("Status").transform.GetChild(i + 2).GetComponent<TextMesh>().text = "yes";
        }
    }

    /// <summary>
    /// Use while recieving .obj files from Slicer to indicate to user the files were recieved and loaded into file directory
    /// </summary>
    public void updateLoadStatus()
    {
        FileInfo objFile;

        for(int i=0;i<assets.Count;i++)
        {
            //if the asset is an obj file, check if it has been loaded into the patient directory
            if (assets[i].Contains(".obj"))
            {
                objFile = new FileInfo(filePath + "/" + assets[i]);
                //update the load status on the menu
                if (objFile.Exists)
                    GameObject.Find("Status").transform.GetChild(i + 2).GetComponent<TextMesh>().text = "yes";
                else
                    GameObject.Find("Status").transform.GetChild(i + 2).GetComponent<TextMesh>().text = "---";
            }
        }
    }
}
